﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }

    public async Task<bool> CheckDocument(string document)
    {
         var check = await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Document == document);

        if (check == null)
            return false;

        return true;
    }

    public SelectList GetSuppliersEnabled()
    {
        return new SelectList(_dbcontext.Suppliers.AsNoTracking().Where(x => x.Active), "Id", "Name");
    }
}
