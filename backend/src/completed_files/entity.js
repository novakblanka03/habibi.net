const {EntitySchema} = require("typeorm");


module.exports = new EntitySchema({
    name: "CompletedFile",
    tableName: "completed_files",
    columns: {
        id: {
            primary: true,
            type: "int",
            generated: true,
        },
        file_id: {
            type: "int",
        },
        user_id: {
            type: "varchar",
        },
        finish_time: {
            type: "string",
        },
        date: {
            type: "string",
        },
        percentage: {
            type: "int",
        },
        evaluation: {
            type: "json",
        },
        notes: {
            type: "json",
        },
    },
    relations: {
        file: {
            type: "many-to-one",
            target: "File",
            joinColumn: { name: "file_id" },
            onDelete: "CASCADE",
        },
        user: {
            type: "many-to-one",
            target: "User",
            joinColumn: { name: "user_id" },
            onDelete: "CASCADE",
        },
    },
});
