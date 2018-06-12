using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Models
{
    public class PigeonsContext : DbContext
    {
        public PigeonsContext(DbContextOptions<PigeonsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<CmsAuthor> CmsAuthor { get; set; }
        public virtual DbSet<CmsClass> CmsClass { get; set; }
        public virtual DbSet<CmsContents> CmsContents { get; set; }
        public virtual DbSet<CmsContribution> CmsContribution { get; set; }
        public virtual DbSet<CmsCritic> CmsCritic { get; set; }
        public virtual DbSet<CmsDocs> CmsDocs { get; set; }
        public virtual DbSet<CmsRolesPower> CmsRolesPower { get; set; }
        public virtual DbSet<LiLive> LiLive { get; set; }
        public virtual DbSet<Live> Live { get; set; }
        public virtual DbSet<TpTopic> TpTopic { get; set; }
        public virtual DbSet<VdBanner> VdBanner { get; set; }
        public virtual DbSet<VdClass> VdClass { get; set; }
        public virtual DbSet<VdSubClass> VdSubClass { get; set; }
        public virtual DbSet<VdTag> VdTag { get; set; }
        public virtual DbSet<VdVideo> VdVideo { get; set; }
        public virtual DbSet<VdVideoCollect> VdVideoCollect { get; set; }
        public virtual DbSet<VdVideoRecord> VdVideoRecord { get; set; }
        public virtual DbSet<VdVideoReview> VdVideoReview { get; set; }

        public virtual DbSet<Dtos.HomeHotSearch> HomeHotSearch { get; set; }
        public virtual DbSet<Dtos.HomeDetail> HomeDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CmsAuthor>(entity =>
            {
                entity.ToTable("CMS_Author");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddTime)
                    .HasColumnName("addTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Articles)
                    .HasColumnName("articles")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AuditTime)
                    .HasColumnName("auditTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AuditUserId)
                    .HasColumnName("auditUserID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Autonym)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ContactMail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ContactTel)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Coverimg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Dislikes).HasDefaultValueSql("((0))");

                entity.Property(e => e.Hits).HasDefaultValueSql("((0))");

                entity.Property(e => e.Info).HasColumnType("text");

                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Rec).HasDefaultValueSql("((0))");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sort).HasDefaultValueSql("((100))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TruthHits).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CmsClass>(entity =>
            {
                entity.HasKey(e => e.CmsCid)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CMS_Class");

                entity.Property(e => e.CmsCid).HasColumnName("CmsCID");

                entity.Property(e => e.Attribs).HasDefaultValueSql("(0)");

                entity.Property(e => e.CmsClick).HasDefaultValueSql("(0)");

                entity.Property(e => e.CmsCname)
                    .HasColumnName("CmsCName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CmsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CmsDesc).HasMaxLength(600);

                entity.Property(e => e.CmsEname)
                    .HasColumnName("CmsEName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CmsFlag).HasDefaultValueSql("(0)");

                entity.Property(e => e.CmsImg)
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.Property(e => e.CmsOrder)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(80);

                entity.Property(e => e.TargetUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('mainFrame')");
            });

            modelBuilder.Entity<CmsContents>(entity =>
            {
                entity.HasKey(e => e.CmsId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CMS_Contents");

                entity.Property(e => e.CmsId).HasColumnName("CmsID");

                entity.Property(e => e.AreaCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Attribs).HasDefaultValueSql("((0))");

                entity.Property(e => e.CmsAuthor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CmsBody).HasColumnType("text");

                entity.Property(e => e.CmsCid).HasColumnName("CmsCID");

                entity.Property(e => e.CmsClick).HasDefaultValueSql("((0))");

                entity.Property(e => e.CmsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CmsKeys)
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.CmsPhotos)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CmsSort)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CmsSource)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CmsStats).HasDefaultValueSql("((0))");

                entity.Property(e => e.CmsTitle)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CmsTitleForDisp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FromMasterId)
                    .HasColumnName("FromMasterID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.MasterRecommend).HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OprateDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OriginalStatementType).HasDefaultValueSql("((0))");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(80);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.TopicId)
                    .HasColumnName("TopicID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VideoPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Wordpress)
                    .HasColumnName("wordpress")
                    .HasColumnType("text")
                    .HasDefaultValueSql("(',,')");

                entity.HasOne(d => d.CmsC)
                    .WithMany(p => p.CmsContents)
                    .HasForeignKey(d => d.CmsCid)
                    .HasConstraintName("R_CMS_Contents_CmsCID");
            });

            modelBuilder.Entity<CmsContribution>(entity =>
            {
                entity.ToTable("CMS_contribution");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Addtime)
                    .HasColumnName("addtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Annotations).HasColumnType("text");

                entity.Property(e => e.AuditTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AuditUserId).HasColumnName("AuditUserID");

                entity.Property(e => e.Autonym)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Body).HasColumnType("text");

                entity.Property(e => e.Cmsid)
                    .HasColumnName("CMSID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ContactMail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ContactTel)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Exclusive).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPayRoyalty)
                    .HasColumnName("isPayRoyalty")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MsgIp)
                    .HasColumnName("MsgIP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Origin).HasDefaultValueSql("((0))");

                entity.Property(e => e.Royalty).HasDefaultValueSql("((0))");

                entity.Property(e => e.SendContent)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SendTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CmsCritic>(entity =>
            {
                entity.HasKey(e => e.CommId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CMS_Critic");

                entity.Property(e => e.CommId).HasColumnName("CommID");

                entity.Property(e => e.CmsId).HasColumnName("CmsID");

                entity.Property(e => e.CriticBody)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CriticTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OprateDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Cms)
                    .WithMany(p => p.CmsCritic)
                    .HasForeignKey(d => d.CmsId)
                    .HasConstraintName("R_CMS_Critic_CmsID");
            });

            modelBuilder.Entity<CmsDocs>(entity =>
            {
                entity.HasKey(e => e.CommId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CMS_Docs");

                entity.Property(e => e.CommId).HasColumnName("CommID");

                entity.Property(e => e.CmsId).HasColumnName("CmsID");

                entity.Property(e => e.DocsPath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DocsType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OprateDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SourceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CmsRolesPower>(entity =>
            {
                entity.HasKey(e => e.CommId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CMS_RolesPower");

                entity.Property(e => e.CommId).HasColumnName("CommID");

                entity.Property(e => e.CmsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CmsPowers)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<LiLive>(entity =>
            {
                entity.ToTable("Li_Live");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuditTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AuditUserId).HasColumnName("AuditUserID");

                entity.Property(e => e.CoverImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Hits).HasDefaultValueSql("((0))");

                entity.Property(e => e.Info).HasColumnType("text");

                entity.Property(e => e.LiveDisplay).HasDefaultValueSql("((1))");

                entity.Property(e => e.LiveJs)
                    .HasColumnName("LiveJS")
                    .HasColumnType("text");

                entity.Property(e => e.LiveLogin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LiveLoginInfo).HasColumnType("text");

                entity.Property(e => e.LiveLoginPw)
                    .HasColumnName("LiveLoginPW")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LiveLoginStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.LiveNum)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LivePlatform).HasDefaultValueSql("((0))");

                entity.Property(e => e.LiveStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.LiveTime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Organizer)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sort).HasDefaultValueSql("((100))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uptime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Live>(entity =>
            {
                entity.ToTable("live");

                entity.Property(e => e.LiveId).HasColumnName("live_id");

                entity.Property(e => e.Attribs).HasColumnName("attribs");

                entity.Property(e => e.CmsCode)
                    .HasColumnName("cms_code")
                    .HasMaxLength(20);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserid).HasColumnName("create_userid");

                entity.Property(e => e.LiberationPoint)
                    .HasColumnName("liberation_point")
                    .HasMaxLength(50);

                entity.Property(e => e.LiveClick).HasDefaultValueSql("((0))");

                entity.Property(e => e.LiveName)
                    .HasColumnName("live_name")
                    .HasMaxLength(200);

                entity.Property(e => e.LiveTime)
                    .HasColumnName("live_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LiveType)
                    .HasColumnName("live_type")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LiveUrl)
                    .HasColumnName("live_url")
                    .HasMaxLength(200);

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(80);

                entity.Property(e => e.SupplierName)
                    .HasColumnName("supplier_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUserid).HasColumnName("update_userid");
            });

            modelBuilder.Entity<TpTopic>(entity =>
            {
                entity.ToTable("TP_Topic");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddTime)
                    .HasColumnName("addTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUserId)
                    .HasColumnName("addUserID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Album1Id)
                    .HasColumnName("Album1_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Album2Id)
                    .HasColumnName("Album2_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Album3Id)
                    .HasColumnName("Album3_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AuditTime)
                    .HasColumnName("auditTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AuditUserId).HasColumnName("auditUserID");

                entity.Property(e => e.BannerImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CoverImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Dislikes).HasDefaultValueSql("((0))");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Hits).HasDefaultValueSql("((0))");

                entity.Property(e => e.Info).HasColumnType("text");

                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.LinkUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LiveDisplay)
                    .HasColumnName("Live_Display")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LiveId)
                    .HasColumnName("Live_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.News1Id)
                    .HasColumnName("News1_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.News2Id)
                    .HasColumnName("News2_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.News3Id)
                    .HasColumnName("News3_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.News4Id)
                    .HasColumnName("News4_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.News5Id)
                    .HasColumnName("News5_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sort)
                    .HasColumnName("sort")
                    .HasDefaultValueSql("((100))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TemplateColor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.TimeInfo)
                    .HasColumnName("Time_Info")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TruthHits).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateId)
                    .HasColumnName("updateID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VideoDisplay)
                    .HasColumnName("Video_Display")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<VdBanner>(entity =>
            {
                entity.ToTable("VD_Banner");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cid).HasDefaultValueSql("((0))");

                entity.Property(e => e.CoverImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sid).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sort).HasDefaultValueSql("((100))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Uptime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WapLinks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.WebLinks)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VdClass>(entity =>
            {
                entity.ToTable("VD_Class");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.NameCn)
                    .IsRequired()
                    .HasColumnName("NameCN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEn)
                    .HasColumnName("NameEN")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PcIcon)
                    .HasColumnName("PC_Icon")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ShowInfo).HasColumnType("text");

                entity.Property(e => e.Sort).HasDefaultValueSql("((100))");

                entity.Property(e => e.WapIcon)
                    .HasColumnName("Wap_Icon")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.WapStyle)
                    .HasColumnName("Wap_Style")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<VdSubClass>(entity =>
            {
                entity.ToTable("VD_SubClass");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cid).HasDefaultValueSql("((0))");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.NameCn)
                    .IsRequired()
                    .HasColumnName("NameCN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEn)
                    .HasColumnName("NameEN")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PcIcon)
                    .HasColumnName("PC_Icon")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ShowInfo).HasColumnType("text");

                entity.Property(e => e.Sort).HasDefaultValueSql("((100))");

                entity.Property(e => e.WapIcon)
                    .HasColumnName("Wap_Icon")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VdTag>(entity =>
            {
                entity.ToTable("VD_TAG");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.NameCn)
                    .IsRequired()
                    .HasColumnName("NameCN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEn)
                    .HasColumnName("NameEN")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sort).HasDefaultValueSql("((100))");
            });

            modelBuilder.Entity<VdVideo>(entity =>
            {
                entity.ToTable("VD_Video");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AirTime)
                    .HasColumnName("airTime")
                    .HasMaxLength(200);

                entity.Property(e => e.AuditReply).HasMaxLength(500);

                entity.Property(e => e.AuditTime).HasColumnType("datetime");

                entity.Property(e => e.AuditUserId).HasColumnName("AuditUserID");

                entity.Property(e => e.Cid).HasDefaultValueSql("((0))");

                entity.Property(e => e.CoverImg).HasMaxLength(500);

                entity.Property(e => e.Dislikes).HasDefaultValueSql("((0))");

                entity.Property(e => e.GuestPerformers).HasMaxLength(50);

                entity.Property(e => e.Hits).HasDefaultValueSql("((0))");

                entity.Property(e => e.HotTopic).HasDefaultValueSql("((0))");

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Moderator).HasMaxLength(50);

                entity.Property(e => e.Rec).HasDefaultValueSql("((0))");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(500);

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(200);

                entity.Property(e => e.Sid).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sort).HasDefaultValueSql("((100))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tags)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasDefaultValueSql("(',,')");

                entity.Property(e => e.Theme).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.TopicId)
                    .HasColumnName("TopicID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TruthHits).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uptime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.VideoAutomate).HasDefaultValueSql("((0))");

                entity.Property(e => e.VideoLength).HasMaxLength(50);

                entity.Property(e => e.VideoReference).HasMaxLength(500);

                entity.Property(e => e.VideoReferenceVid).HasMaxLength(500);

                entity.Property(e => e.VideoSource).HasMaxLength(500);
            });

            modelBuilder.Entity<VdVideoCollect>(entity =>
            {
                entity.ToTable("VD_VideoCollect");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");
            });

            modelBuilder.Entity<VdVideoRecord>(entity =>
            {
                entity.ToTable("VD_VideoRecord");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");
            });

            modelBuilder.Entity<VdVideoReview>(entity =>
            {
                entity.ToTable("VD_VideoReview");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Agree)
                    .HasColumnName("agree")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AuditTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AuditUserId).HasColumnName("AuditUserID");

                entity.Property(e => e.Disagree)
                    .HasColumnName("disagree")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRead)
                    .HasColumnName("isRead")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MediaType).HasDefaultValueSql("((0))");

                entity.Property(e => e.MsgIp)
                    .HasColumnName("MsgIP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NewReply)
                    .HasColumnName("newReply")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReContent).HasColumnType("ntext");

                entity.Property(e => e.ReId)
                    .HasColumnName("ReID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReId2)
                    .HasColumnName("ReID2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReId3)
                    .HasColumnName("ReID3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.ToUserId)
                    .HasColumnName("ToUserID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Uptime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");
            });
        }
    }
}
