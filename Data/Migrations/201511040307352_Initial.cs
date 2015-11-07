namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Team1ID = c.Int(nullable: false),
                        Team2ID = c.Int(nullable: false),
                        IsTeam1Bowl = c.Boolean(nullable: false),
                        IsTeam2Bowl = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MatchID)
                .ForeignKey("dbo.Team", t => t.Team1ID, cascadeDelete: false)
                .ForeignKey("dbo.Team", t => t.Team2ID, cascadeDelete: false)
                .Index(t => t.Team1ID)
                .Index(t => t.Team2ID);
            
            CreateTable(
                "dbo.Over",
                c => new
                    {
                        OverID = c.Int(nullable: false, identity: true),
                        MatchID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false),
                        OverNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OverID)
                .ForeignKey("dbo.Match", t => t.MatchID, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.MatchID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.OverDetails",
                c => new
                    {
                        OverDetailsID = c.Int(nullable: false, identity: true),
                        OverID = c.Int(nullable: false),
                        BallNumber = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 128),
                        RunTaken = c.Int(nullable: false),
                        IsWide = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OverDetailsID)
                .ForeignKey("dbo.Over", t => t.OverID, cascadeDelete: true)
                .Index(t => t.OverID);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TeamID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "Team2ID", "dbo.Team");
            DropForeignKey("dbo.Match", "Team1ID", "dbo.Team");
            DropForeignKey("dbo.Over", "TeamID", "dbo.Team");
            DropForeignKey("dbo.OverDetails", "OverID", "dbo.Over");
            DropForeignKey("dbo.Over", "MatchID", "dbo.Match");
            DropIndex("dbo.OverDetails", new[] { "OverID" });
            DropIndex("dbo.Over", new[] { "TeamID" });
            DropIndex("dbo.Over", new[] { "MatchID" });
            DropIndex("dbo.Match", new[] { "Team2ID" });
            DropIndex("dbo.Match", new[] { "Team1ID" });
            DropTable("dbo.Team");
            DropTable("dbo.OverDetails");
            DropTable("dbo.Over");
            DropTable("dbo.Match");
        }
    }
}
