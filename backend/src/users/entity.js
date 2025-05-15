const { EntitySchema } = require("typeorm");

module.exports = new EntitySchema({
    name: "User",
    tableName: "users",
    columns: {
        uid: {
            primary: true,
            type: "varchar",
        },
        email: {
            type: "varchar",
            unique: true,
        },
    }
});
