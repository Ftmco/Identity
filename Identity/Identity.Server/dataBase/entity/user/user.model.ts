import { DataTypes, Sequelize } from "sequelize";


module.exports = (sequelize: Sequelize) => {
    sequelize.define("User", {
        Id: {
            type: DataTypes.UUID,
            defaultValue: DataTypes.UUID.key,
            autoIncrementIdentity: true,
            primaryKey: true,
            allowNull: false
        },
        UserName: {
            type: DataTypes.STRING,
            allowNull: false,
        },
        FirstName: {
            type: DataTypes.STRING,
        },
        LastName: {
            type: DataTypes.STRING,
        },
        MobileNo: {
            type: DataTypes.STRING('12'),
        },
        Email: {
            type: DataTypes.STRING,
        },
        Password: {
            type: DataTypes.STRING,
            allowNull: false
        },
        IsActive: {
            type: DataTypes.BOOLEAN,
            allowNull: false,
            defaultValue: false,
        },
        ActiveCode: {
            type: DataTypes.STRING(7),
            allowNull: false
        },
        ActiveData: {
            type: DataTypes.DATE,
            allowNull: false
        }
    })
}