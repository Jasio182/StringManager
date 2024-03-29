﻿using Microsoft.EntityFrameworkCore;
using StringManager.Core.Enums;
using StringManager.DataAccess.Entities;
using System.Threading.Tasks;

namespace StringManager.DataAccess.CQRS.Queries
{
    public class GetMyInstrumentQuery : QueryBase<MyInstrument>
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public AccountType AccountType { get; set; }

        public override async Task<MyInstrument> Execute(StringManagerStorageContext context)
        {
            try
            {
                return await context.MyInstruments
                    .Include(myInstrument => myInstrument.User)
                    .Include(myInstrument => myInstrument.Instrument)
                    .ThenInclude(instrument => instrument.Manufacturer)
                    .Include(myInstrument => myInstrument.InstalledStrings)
                    .ThenInclude(installedString => installedString.String)
                    .Include(myInstrument => myInstrument.InstalledStrings)
                    .ThenInclude(installedString => installedString.Tone)
                    .FirstAsync(myInstrument => myInstrument.Id == Id
                        && (myInstrument.User.Id == UserId || AccountType == AccountType.Admin));
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }
    }
}
