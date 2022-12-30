using FluentMigrator;

namespace dispatchservice.migrations
{
    [Migration(1)]
    public class V0001 : Migration
    {
        public override void Up()
        {
            Create.Table("Service")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Price").AsDecimal(18,2).NotNullable()
                .WithColumn("Deleted").AsBoolean().NotNullable();

            Create.Table("Estate")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Type").AsString(50).NotNullable()
                .WithColumn("AOGUID").AsString(25).Nullable();


            Create.Table("Street")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Type").AsString(50).NotNullable()
                .WithColumn("AOGUID").AsString(25).Nullable();


            Create.Table("House")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Number").AsString(25).NotNullable()
                .WithColumn("EstateId").AsGuid().Nullable().ForeignKey("Estate", "Id")
                .WithColumn("StreetId").AsGuid().NotNullable().ForeignKey("Street", "Id")
                .WithColumn("Deleted").AsBoolean().NotNullable()
                .WithColumn("AOGUID").AsString(25).Nullable()
                .WithColumn("HOUSENUM").AsString(25).Nullable()
                .WithColumn("BUILDNUM").AsString(25).Nullable()
                .WithColumn("STRUCNUM").AsString(25).Nullable()
                .WithColumn("ESTSTATUS").AsInt16().Nullable()
                .WithColumn("STRSTATUS").AsInt16().Nullable();

            Create.Table("Injener")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Deleted").AsBoolean().NotNullable();

            Create.Table("InjenerHouse")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("InjenerId").AsGuid().Nullable().ForeignKey("Injener", "Id") //.PrimaryKey()
                .WithColumn("HouseId").AsGuid().Nullable().ForeignKey("House", "Id"); //.PrimaryKey();

            Create.Table("Ticket")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Number").AsInt32().NotNullable().Identity()
                .WithColumn("ServiceId").AsGuid().NotNullable().ForeignKey("Service", "Id")
                .WithColumn("HouseId").AsGuid().NotNullable().ForeignKey("House", "Id")
                .WithColumn("InjenerId").AsGuid().Nullable().ForeignKey("Injener", "Id")
                .WithColumn("Flat").AsString(10).NotNullable()
                .WithColumn("DateTime").AsDateTime().NotNullable()
                .WithColumn("DateCancel").AsDate().Nullable()
                .WithColumn("DateExecute").AsDate().Nullable()
                .WithColumn("Phone").AsString(25).NotNullable().WithDefaultValue("")
                .WithColumn("MobilePhone").AsString(25).NotNullable().WithDefaultValue("")
                .WithColumn("Price").AsDecimal(18, 2).NotNullable();

        }
        
        public override void Down()
        {
            Delete.Table("Service");
            Delete.Table("Estate");
            Delete.Table("Street");
            Delete.Table("House");
            Delete.Table("Injener");
            Delete.Table("InjenerHouse");
            Delete.Table("Ticket");

        }
    }
}
