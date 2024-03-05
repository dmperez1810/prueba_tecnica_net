namespace OpenData.Persistence.Context;

using Microsoft.EntityFrameworkCore;
using OpenData.Persistence.Entities;

public class OpenDataContext: DbContext
{
	public OpenDataContext(DbContextOptions<OpenDataContext> options): base(options)
	{
	}

	public virtual DbSet<PartMarket>? PartMarkets { get; set; }
}

