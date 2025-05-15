const { EntitySchema } = require("typeorm");

module.exports = new EntitySchema({
    name: "File",
    tableName: "files",
    columns: {
        id: {
            primary: true,
            type: "int",
            generated: true,
        },
        name: {
            type: "varchar",
            unique: true,
        },
        data: {
            type: "bytea",
        },
        year: {
            type: "int",
        },
        subject: {
            type: "varchar",
        },
        examType: {
            type: "varchar",
        },
        docType: {
            type: "varchar",
        },
        createdAt: {
            type: "timestamp",
            default: () => "CURRENT_TIMESTAMP",
        },
        baremId: {
            type: "int",
            nullable: true,
        },
    },
    relations: {
        barem: {
            type: "one-to-one",
            target: "File",
            joinColumn: { name: "baremId" },
            nullable: true,
            onDelete: "SET NULL",
        },
    },
});
