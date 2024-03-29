﻿using Microsoft.EntityFrameworkCore;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Commands
{
    public class AddToneInTuningCommand : CommandBase<ToneInTuning, ToneInTuning>
    {
        public override async Task<ToneInTuning> Execute(StringManagerStorageContext context)
        {
            await context.TonesInTunings.AddAsync(Parameter);
            await context.SaveChangesAsync();
            return Parameter;
        }
    }
}
