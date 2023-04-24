using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestorAlquilerApi.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cif = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Population = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Registration = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    ConfirmationPassword = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BankAccount = table.Column<string>(type: "TEXT", nullable: true),
                    Rol = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Registration = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    FuelType = table.Column<string>(type: "TEXT", nullable: true),
                    GearShiftType = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CarsAvailables = table.Column<int>(type: "INTEGER", nullable: false),
                    CarCategory = table.Column<string>(type: "TEXT", nullable: true),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planning_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CarCategory = table.Column<string>(type: "TEXT", nullable: true),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Branch",
                columns: new[] { "Id", "Address", "Cif", "Country", "Name", "Population" },
                values: new object[,]
                {
                    { 1, "C. de Berlín, 2, 03503 Benidorm, Alicante", "C-124582", "Spain", "Centauro Car hire Benidorm", "Benidorm" },
                    { 2, "Alicante Airport, 03195 Elx, Alicante", "C-124625", "Spain", "Centauro Car hire Alicante airport", "Alicante" },
                    { 3, "Av. de las Cortes Valencianas, 6, 03183 Torrevieja, Alicante", "C-126385", "Spain", "Centauro Car Rental Torrevieja", "Torrevieja" },
                    { 4, "Av. Severiano Falcão 2, Prior Velho, 2685-378 Lisboa, Portugal", "C-124758", "Portugal", "Centauro Location voiture Lisbonne", "Lisboa" },
                    { 5, "Via Portuense, 2483, 00054 Fiumicino RM, Italia", "C-123485", "Italia", "Centauro Rent a Car Rome Airport Fiumicino", "Roma" }
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "BankAccount", "ConfirmationPassword", "Email", "LastName", "Name", "Password", "PhoneNumber", "Registration", "Rol" },
                values: new object[,]
                {
                    { 1, "1234Bank", "$2a$11$fTBkLBVJYf.GtMGVjUr5R.2BY0x6MIEaPIK5Oq6ImroFrP5J0owFa", "mh.haidor@gmail.com", "Hicho Haidor", "Muhammad", "$2a$11$yaTKOu2DRCNELKRF4n67PebmdvivH7/KDmv1LLhseORCk50YNaMiK", 686601702, "45113560A", "Admin" },
                    { 2, "1234Bank", "$2a$11$9Ezwl4F6yJtLBBNcsphJkeWkax21tg24wHztSpAGB2XXHiDs3YePu", "jhon.doe@gmail.com", "Doe", "Jhon", "$2a$11$mBkYJQ7Ugru4UJv5zWBs8OaKrxiCgsPAMMDW/mMHpxkPVe3iG9aJS", 695632458, "45223659F", "User" },
                    { 3, "1234Bank", "$2a$11$BmaZpGBvGmiAF/JdiOLoHeLjfxxnALPHAd4mwyDp9bkay4366rj7C", "jane.doe@gmail.com", "Doe", "Jane", "$2a$11$Gmlux9WPAjxsWBHNQVK8juZemiqKeoqIPpoVxRdp4.bglHQbEnIGG", 695632458, "45369875G", "User" }
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "BranchId", "Brand", "Category", "FuelType", "GearShiftType", "Image", "Model", "Registration" },
                values: new object[,]
                {
                    { 1, 1, "Fiat", "A", "Gasoline", "Manual", "Fiat500Image", "500", "3993-YLH" },
                    { 2, 2, "Fiat", "A", "Gasoline", "Manual", "Fiat500Image", "500", "2519-YES" },
                    { 3, 3, "Fiat", "A", "Diesel", "Manual", "Fiat500Image", "500", "8324-TJS" },
                    { 4, 4, "Fiat", "A", "Gasoline", "Automatic", "Fiat500Image", "500", "0035-LQJ" },
                    { 5, 5, "Fiat", "A", "Gasoline", "Manual", "Fiat500Image", "500", "4596-TCK" },
                    { 6, 1, "Kia", "C", "Diesel", "Manual", "KiaCeedImage", "Ceed", "3960-XXN" },
                    { 7, 2, "Kia", "C", "Gasoline", "Manual", "KiaCeedImage", "Ceed", "3063-GIR" },
                    { 8, 3, "Kia", "C", "Gasoline", "Automatic", "KiaCeedImage", "Ceed", "1551-DLN" },
                    { 9, 4, "Kia", "C", "Gasoline", "Manual", "KiaCeedImage", "Ceed", "1547-SPJ" },
                    { 10, 5, "Kia", "C", "Diesel", "Automatic", "KiaCeedImage", "Ceed", "0725-TMW" },
                    { 11, 1, "Volkswagen", "D", "Diesel", "Automatic", "VolkswagenTRocImage", "T-ROC", "3967-QPV" },
                    { 12, 2, "Volkswagen", "D", "Gasoline", "Manual", "VolkswagenTRocImage", "T-ROC", "9125-NUJ" },
                    { 13, 3, "Volkswagen", "D", "Diesel", "Automatic", "VolkswagenTRocImage", "T-ROC", "6997-QVO" },
                    { 14, 4, "Volkswagen", "D", "Gasoline", "Manual", "VolkswagenTRocImage", "T-ROC", "3315-QUI" },
                    { 15, 5, "Volkswagen", "D", "Diesel", "Automatic", "VolkswagenTRocImage", "T-ROC", "9243-IAC" },
                    { 16, 1, "Hyundai", "D", "Diesel", "Automatic", "HyundaiKonaImage", "Kona", "3013-EKU" },
                    { 17, 2, "Hyundai", "D", "Diesel", "Manual", "HyundaiKonaImage", "Kona", "8640-AZH" },
                    { 18, 3, "Hyundai", "D", "Diesel", "Automatic", "HyundaiKonaImage", "Kona", "1540-QQM" },
                    { 19, 4, "Hyundai", "D", "Gasoline", "Manual", "HyundaiKonaImage", "Kona", "9947-WES" },
                    { 20, 5, "Hyundai", "D", "Diesel", "Automatic", "HyundaiKonaImage", "Kona", "7365-BZL" },
                    { 21, 1, "Ford", "B", "Diesel", "Manual", "FordTransitImage", "Transit ", "5752-PLO" },
                    { 22, 2, "Ford", "B", "Diesel", "Manual", "FordTransitImage", "Transit ", "5755-FIO" },
                    { 23, 3, "Ford", "B", "Diesel", "Manual", "FordTransitImage", "Transit ", "8673-QLF" },
                    { 24, 4, "Ford", "B", "Diesel", "Manual", "FordTransitImage", "Transit ", "0332-GYO" },
                    { 25, 5, "Ford", "B", "Diesel", "Manual", "FordTransitImage", "Transit ", "3145-WEU" },
                    { 26, 1, "Nissan", "A", "Gasoline", "Manual", "NissanSkylineImage", "Skyline ", "4609-KFD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Cif",
                table: "Branch",
                column: "Cif",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_BranchId",
                table: "Car",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_Registration",
                table: "Car",
                column: "Registration",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Email",
                table: "Client",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Registration",
                table: "Client",
                column: "Registration",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planning_BranchId",
                table: "Planning",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClientId",
                table: "Reservation",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Planning");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
