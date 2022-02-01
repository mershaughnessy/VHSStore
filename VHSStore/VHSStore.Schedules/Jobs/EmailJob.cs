using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces;

namespace VHSStore.Schedules.Jobs
{
    public class EmailJob : IEmailJob
    {
        public async Task NewsLetterEmail()
        {
            // Foreach User in DB that has subscribed to newsletter, Email news letter every week.
        }
    }
}
