using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public  record ServiceSettingsDTO(int BaseWindowInMinutes,
        int BasePricePerWindow,
        TimeOnly MorningStart,
        TimeOnly MorningFinish,
        TimeOnly WholeDayStart,
        TimeOnly WholeDayFinish 
        );
}
