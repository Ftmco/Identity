"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const express = require("express");
const path = require("path");
const util_1 = require("util");
const bodyParser = require("body-parser");
const index_1 = require("./dataBase/context/index");
const debug = require('debug')('my express app');
const app = express();
// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'pug');
app.use(express.static(path.join(__dirname, 'public')));
app.use(express.json({ limit: '100mb' }));
app.use(express.urlencoded({ limit: '100mb' }));
app.use(bodyParser.urlencoded({ extended: true, limit: '100mb' }));
app.use(bodyParser.json({ limit: '100mb' }));
//Data Base Config 
const assertDataBaseOk = () => __awaiter(void 0, void 0, void 0, function* () {
    (0, util_1.log)("Checking Data Base Connection ...");
    try {
        yield index_1.default.authenticate();
        (0, util_1.log)("Connection To Data Base Successfully");
    }
    catch (e) {
        (0, util_1.log)("Unable to connect to Data Base");
        (0, util_1.log)(e.message);
        process.exit(1);
    }
});
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
    app.use((err, req, res, next) => {
        res.status(err['status'] || 500);
        res.json();
    });
}
// production error handler
// no stacktraces leaked to user
app.use((err, req, res, next) => {
    res.status(err.status || 500);
    res.json();
});
app.set('port', process.env.PORT || 3000);
const server = app.listen(app.get('port'), () => __awaiter(void 0, void 0, void 0, function* () {
    yield assertDataBaseOk();
    let port = server.address().port;
    (0, util_1.log)(`Express server listening on port ${port}`);
    (0, util_1.log)(`http://localhost:${port}`);
}));
//# sourceMappingURL=app.js.map