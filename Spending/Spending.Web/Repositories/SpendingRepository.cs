using Microsoft.EntityFrameworkCore;
using Spending.Web.Infrastructure;
using Spending.Web.Models;

namespace Spending.Web.Repositories;

public class SpendingRepository : ISpendingRepository
{
    private SpendingDbContext _context;

    public SpendingRepository(SpendingDbContext context)
    { _context = context; }

    public async Task<IEnumerable<SpendingModel>> GetSpending(int month, int year, bool withTemplate = false)
    {
        string mon = month < 10 ? "0" + month.ToString() : month.ToString();
        string yr = year.ToString();

        List<SpendingModel> data = new List<SpendingModel>();

        var current = await _context.spendings.Where(s => s.Month.Equals(mon) && s.Year.Equals(yr)).SingleOrDefaultAsync();
        data.Add(current);

        if (withTemplate)
        {
            var template = await _context.spendings.Where(s => s.Period.Equals("00-2000")).SingleOrDefaultAsync();
            data.Add(template);
        }

        return data;
    }

    public async Task<IEnumerable<SpendingModel>> GetTemplate()
    {
        List<SpendingModel> data = new List<SpendingModel>();

        var template = await _context.spendings.Where(s => s.Description.Equals("Template")).SingleOrDefaultAsync();
        data.Add(template);

        return data;
    }
}

public interface ISpendingRepository
{
    public Task<IEnumerable<SpendingModel>> GetSpending(int month, int year, bool withTemplate = true);

    public Task<IEnumerable<SpendingModel>> GetTemplate();

}