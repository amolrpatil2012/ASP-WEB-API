﻿using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext( DbContextOptions<ApplicationDBContext> options):base(options) 
    { }
    
    public DbSet<Product> Products { get; set; }
}
