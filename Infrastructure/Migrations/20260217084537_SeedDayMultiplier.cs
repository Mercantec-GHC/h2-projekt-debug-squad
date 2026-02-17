using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDayMultiplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""DayMultipliers"" (""Id"", ""Day"", ""Multiplier"") VALUES
                (1, 1, 1.0),
                (2, 2, 1.0),
                (3, 3, 1.0),
                (4, 4, 1.0),
                (5, 5, 1.0),
                (6, 6, 1.0),
                (7, 0, 1.0);
            ");

            migrationBuilder.Sql(@"
                SELECT setval(
                    pg_get_serial_sequence('""DayMultipliers""','Id'),
                    7,
                    true
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM ""DayMultipliers"";");
        }
    }
}
