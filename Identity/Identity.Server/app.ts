import * as express from 'express';
import { AddressInfo } from "net";
import * as path from 'path';
import { log } from 'util';
import * as bodyParser from 'body-parser'
import sequelize from './dataBase/context/index'


const debug = require('debug')('my express app');
const app = express();

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'pug');

app.use(express.static(path.join(__dirname, 'public')));
app.use(express.json({ limit: '100mb' }))
app.use(express.urlencoded({ limit: '100mb' }))
app.use(bodyParser.urlencoded({ extended: true, limit: '100mb' }))
app.use(bodyParser.json({ limit: '100mb' }))


//Data Base Config 
const assertDataBaseOk = async () => {
    log("Checking Data Base Connection ...")
    try {
        await sequelize.authenticate();
        log("Connection To Data Base Successfully")
    }
    catch (e) {
        log("Unable to connect to Data Base")
        log(e.message)
        process.exit(1)
    }
}

// catch 404 and forward to error handler
app.use((req, res, next) => {
    const err = new Error('Not Found');
    err['status'] = 404;
    next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use((err, req, res, next) => { // eslint-disable-line @typescript-eslint/no-unused-vars
        res.status(err['status'] || 500);
        res.json();
    });
}

// production error handler
// no stacktraces leaked to user
app.use((err, req, res, next) => { // eslint-disable-line @typescript-eslint/no-unused-vars
    res.status(err.status || 500);
    res.json();
});

app.set('port', process.env.PORT || 3000);

const server = app.listen(app.get('port'), async () => {
    await assertDataBaseOk()
    let port = (server.address() as AddressInfo).port;
    log(`Express server listening on port ${port}`);
    log(`http://localhost:${port}`)
});

