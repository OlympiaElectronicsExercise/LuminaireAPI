using API.Data;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace API.Controllers.Luminaire
{
    public class DashboardController : BaseApiController
    {
        private readonly ILogger<LuminaireController> _logger;
        private readonly DatabaseContext _context;
        public DashboardController(ILogger<LuminaireController> logger, DatabaseContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("TestStatus/{testKind}")]
        public async Task<ActionResult<int>> GetTestStatus(int testKind)
        {
            const int BATTERY_TESTKIND = 1;
            const int LAMP_TESTKIND = 2;

            string fieldName = string.Empty;

            switch (testKind)
            {
                case BATTERY_TESTKIND:
                    fieldName = "BatteryTestRunning";
                    break;

                case LAMP_TESTKIND:
                    fieldName = "LampTestRunning";
                    break;
            }

            string queryString = String.Format("select count(*) as Value from Luminaires where {0} = 1 and UpdateOn = DATE('now')", fieldName);

            FormattableString queryFormattableString = FormattableStringFactory.Create(queryString);

            var tc = await _context.Database.SqlQuery<TotalCntDto>(queryFormattableString)
                                        .FirstAsync();

            return tc.Value;
        }

        [HttpGet("FaultProgress/{faultKind}")]
        public async Task<ActionResult<ProgressDto>> GetFaultProgress(int faultKind)
        {
            const int BATTERYCAPACITY_FAULTKIND = 1;
            const int LAMP_FAULTKIND = 2;
            const int CHARGER_FAULTKIND = 3;
            const int MAINS_FAULTKIND = 4;

            string fieldName = string.Empty;

            switch (faultKind)
            {
                case BATTERYCAPACITY_FAULTKIND:
                    fieldName = "BatteryCapacityFault";
                    break;

                case LAMP_FAULTKIND:
                    fieldName = "LampFault";
                    break;

                case CHARGER_FAULTKIND:
                    fieldName = "ChargerFault";
                    break;

                case MAINS_FAULTKIND:
                    fieldName = "MainsFault";
                    break;
            }

            ProgressDto progress = new ProgressDto();

            // Get current Total Count

            string queryString = String.Format("select count(*) as Value from Luminaires where {0} = 1 and UpdateOn = DATE('now')", fieldName);

            FormattableString queryFormattableString = FormattableStringFactory.Create(queryString);

            var tcCurrent = await _context.Database.SqlQuery<TotalCntDto>(queryFormattableString)
                                        .FirstAsync();

            // Get previous Total Count

            queryString = String.Format("select count(*) as Value from Luminaires where {0} = 1 and UpdateOn = DATE('now','-1 day')", fieldName);

            queryFormattableString = FormattableStringFactory.Create(queryString);

            var tcPrevious = await _context.Database.SqlQuery<TotalCntDto>(queryFormattableString)
                                        .FirstAsync();

            // Calc the increasing/descreasing Percent, Trend and Status

            progress.Value = tcCurrent.Value;
            if (tcPrevious.Value != 0)
            {
                progress.Percent = (int)(((double)tcCurrent.Value - (double)tcPrevious.Value) / (double)tcPrevious.Value * 100.0);
            }
            else
            {
                progress.Percent = 0;
            }

            if (progress.Percent > 0)
            {
                progress.Trend = "up";
                progress.Status = "red";
            }
            else
            {
                progress.Percent = Math.Abs(progress.Percent);
                progress.Trend = "down";
                progress.Status = "teal";
            }

            return progress;
        }

        [HttpGet("TotalFaultsPerAddress")]
        public async Task<ActionResult<List<FaultTotalCntPerAddressDto>>> GetTotalFaultsPerAddress()
        {
            List<FaultTotalCntPerAddressDto> ftpa = new List<FaultTotalCntPerAddressDto>() {
                new FaultTotalCntPerAddressDto { Address = 1, TotalCnt = 5 },
                new FaultTotalCntPerAddressDto { Address = 2, TotalCnt = 7 }
            };

            return await Task.FromResult(ftpa);
        }

    }
}