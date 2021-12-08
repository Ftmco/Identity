import { log } from "console";
import { Sequelize } from "sequelize";
import applyExtraSetup from "./extraSetup";


const sequelize = new Sequelize("Identity_Db", "amir", "1G14ijWA", {
    host: "localhost",
    dialect: "mssql",
    benchmark: true
})

const modelDefines = [
    require("../entity/user/user.model")
]

for (const modelDefien of modelDefines) {
    modelDefien(sequelize)
}

applyExtraSetup(sequelize);

sequelize.sync({ alter: true }).then((success) => {
    log("Success to sync data base")
    log(success.models);
}).catch((e) => {
    log("Exception in sync data base...")
    log(e.message)
})

export default sequelize;