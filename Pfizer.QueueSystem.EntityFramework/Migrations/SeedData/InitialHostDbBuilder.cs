using Pfizer.QueueSystem.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Pfizer.QueueSystem.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly QueueSystemDbContext _context;

        public InitialHostDbBuilder(QueueSystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
