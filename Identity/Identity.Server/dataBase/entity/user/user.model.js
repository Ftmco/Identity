"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const sequelize_1 = require("sequelize");
module.exports = (sequelize) => {
    sequelize.define("User", {
        Id: {
            type: sequelize_1.DataTypes.UUID,
            defaultValue: sequelize_1.DataTypes.UUID.key,
            autoIncrementIdentity: true,
            primaryKey: true,
            allowNull: false
        },
        UserName: {
            type: sequelize_1.DataTypes.STRING,
            allowNull: false,
        },
        FirstName: {
            type: sequelize_1.DataTypes.STRING,
        },
        LastName: {
            type: sequelize_1.DataTypes.STRING,
        },
        MobileNo: {
            type: sequelize_1.DataTypes.STRING('12'),
        },
        Email: {
            type: sequelize_1.DataTypes.STRING,
        },
        Password: {
            type: sequelize_1.DataTypes.STRING,
            allowNull: false
        },
        IsActive: {
            type: sequelize_1.DataTypes.BOOLEAN,
            allowNull: false,
            defaultValue: false,
        },
        ActiveCode: {
            type: sequelize_1.DataTypes.STRING(7),
            allowNull: false
        },
        ActiveData: {
            type: sequelize_1.DataTypes.DATE,
            allowNull: false
        }
    });
};
//# sourceMappingURL=user.model.js.map