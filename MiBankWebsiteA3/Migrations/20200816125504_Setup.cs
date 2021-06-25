using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiBankWebsiteA3.Migrations
{
    public partial class Setup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    TFN = table.Column<string>(maxLength: 11, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    State = table.Column<string>(maxLength: 3, nullable: true),
                    PostCode = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.CheckConstraint("CH_Customer_CustomerID", "len(CustomerID) = 4");
                    table.CheckConstraint("CH_Customer_Name", "len(Name) <= 50");
                    table.CheckConstraint("CH_Customer_FTN", "len(TFN) <= 11");
                    table.CheckConstraint("CH_Customer_Address", "len(Address) <= 50");
                    table.CheckConstraint("CH_Customer_City", "len(City) <= 40");
                    table.CheckConstraint("CH_Customer_Phone", "len(Phone) <= 15");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNumber = table.Column<int>(nullable: false),
                    AccountType = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNumber);
                    table.CheckConstraint("CH_Account_Balance", "Balance >= 0");
                    table.CheckConstraint("CH_Account_CustomerID", "len(CustomerID) = 4");
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginID = table.Column<int>(maxLength: 8, nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    ModifyDate = table.Column<DateTime>(maxLength: 8, nullable: false),
                    Locked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginID);
                    table.CheckConstraint("CH_Login_LoginID", "len(LoginID) = 8");
                    table.CheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
                    table.CheckConstraint("CH_Login_CustomerID", "len(CustomerID) = 4");
                    table.ForeignKey(
                        name: "FK_Logins_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payees",
                columns: table => new
                {
                    PayeeID = table.Column<int>(maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostCode = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payees", x => x.PayeeID);
                    table.ForeignKey(
                        name: "FK_Payees_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<int>(maxLength: 4, nullable: false),
                    DestAccountNumber = table.Column<int>(maxLength: 4, nullable: true),
                    Amount = table.Column<decimal>(type: "Money", maxLength: 8, nullable: false),
                    Comment = table.Column<string>(maxLength: 50, nullable: false),
                    TransactionTimeUtc = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.CheckConstraint("CH_Transaction_Comment", "Len(Comment) <= 50");
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_DestAccountNumber",
                        column: x => x.DestAccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPays",
                columns: table => new
                {
                    BillPayID = table.Column<int>(maxLength: 4, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<int>(maxLength: 4, nullable: false),
                    PayeeID = table.Column<int>(maxLength: 4, nullable: true),
                    Amount = table.Column<decimal>(maxLength: 8, nullable: false),
                    ScheduleDate = table.Column<DateTime>(nullable: false),
                    PayPeriod = table.Column<int>(nullable: false),
                    Blocked = table.Column<bool>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPays", x => x.BillPayID);
                    table.ForeignKey(
                        name: "FK_BillPays_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillPays_Payees_PayeeID",
                        column: x => x.PayeeID,
                        principalTable: "Payees",
                        principalColumn: "PayeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_AccountNumber",
                table: "BillPays",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_PayeeID",
                table: "BillPays",
                column: "PayeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Payees_CustomerID",
                table: "Payees",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumber",
                table: "Transactions",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestAccountNumber",
                table: "Transactions",
                column: "DestAccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPays");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Payees");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
