using System.ComponentModel.DataAnnotations;

namespace Spending.Web.Models;


public class SpendingModel
{
    public Guid Id { get; set; }    
    public string Period { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
    public string Description { get; set; }
    public List<Item>? Items { get; set; }
}

public class Item
{
    public string Name { get; set; }
    public string Group { get; set; }
    public string Total { get; set; }
    public IList<Detail>? Details { get; set; }
}

public class Detail
{
    public string Time { get; set; }
    public string Amount { get; set; }
}

