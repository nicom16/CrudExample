using Microsoft.EntityFrameworkCore;

namespace CrudExample.Dal.Context;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    // TODO mapping etc
}