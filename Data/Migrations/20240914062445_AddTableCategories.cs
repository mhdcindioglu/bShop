using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCategories : Migration
    {
        /// <inheritdoc />
        string tableName = "Categories";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: tableName,
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            Dictionary<string, string> categories = new()
            {
                { "Accessories", "accessories" },
                { "Camera", "camera" },
                { "Computer", "computer" },
                { "Fan", "fan" },
                { "Headphones", "headphones" },
                { "Health And Beauty", "health-and-beauty" },
                { "Jewelry & Watch", "jewelry-watch" },
                { "Laptop", "laptop" },
                { "Mobile", "mobile" },
                { "Monitor", "monitor" },
                { "Motor Bike", "motor-bike" },
                { "Shoes", "shoes" },
                { "TV & Videos", "tv-videos" },
                { "Wireless Speakers", "wireless-speakers" },
            };
            
            foreach (var category in categories)
                migrationBuilder.Sql($"INSERT INTO {tableName} (Name, Slug) VALUES ('{category.Key}', '{category.Value}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: tableName);
        }
    }
}
