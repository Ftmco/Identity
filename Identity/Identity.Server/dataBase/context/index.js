"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const console_1 = require("console");
const sequelize_1 = require("sequelize");
const extraSetup_1 = require("./extraSetup");
const sequelize = new sequelize_1.Sequelize("Identity_Db", "amir", "1G14ijWA", {
    host: "localhost",
    dialect: "mssql",
    benchmark: true
});
const modelDefines = [
    require("../entity/user/user.model")
];
for (const modelDefien of modelDefines) {
    modelDefien(sequelize);
}
(0, extraSetup_1.default)(sequelize);
sequelize.sync({ force: true }).then((success) => {
    (0, console_1.log)("Success to sync data base");
    (0, console_1.log)(success.models);
}).catch((e) => {
    (0, console_1.log)("Exception in sync data base...");
    (0, console_1.log)(e.message);
});
exports.default = sequelize;
//# sourceMappingURL=index.js.map