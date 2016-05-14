namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchType = c.Int(nullable: false),
                        Winner = c.Boolean(nullable: false),
                        TeamA_Id = c.Int(nullable: false),
                        TeamB_Id = c.Int(nullable: false),
                        Tournament_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamA_Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.TeamB_Id, cascadeDelete: false)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id, cascadeDelete: true)
                .Index(t => t.TeamA_Id)
                .Index(t => t.TeamB_Id)
                .Index(t => t.Tournament_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.Tournament_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Age = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Matches", "TeamB_Id", "dbo.Teams");
            DropForeignKey("dbo.Matches", "TeamA_Id", "dbo.Teams");
            DropForeignKey("dbo.Players", "Team_Id", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "Tournament_Id" });
            DropIndex("dbo.Matches", new[] { "Tournament_Id" });
            DropIndex("dbo.Matches", new[] { "TeamB_Id" });
            DropIndex("dbo.Matches", new[] { "TeamA_Id" });
            DropTable("dbo.Tournaments");
            DropTable("dbo.Players");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
        }
    }
}
