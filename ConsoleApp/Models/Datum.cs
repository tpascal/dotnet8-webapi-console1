using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp.Models
{
    public class Datum
    {
        public int sort_order { get; set; } = 0;
        public int wmo { get; set; } = 0;
        public string? name { get; set; } = string.Empty;
        public string? history_product { get; set; } = string.Empty;
        public string? local_date_time { get; set; } = string.Empty;
        public string? local_date_time_full { get; set; } = string.Empty;
        public string? aifstime_utc { get; set; } = string.Empty;
        public double lat { get; set; } = 0;
        public double lon { get; set; } = 0;
        public double? apparent_t { get; set; } = 0;
        public string? cloud { get; set; } = string.Empty;
        public int? cloud_base_m { get; set; } = 0;
        public int? cloud_oktas { get; set; } = 0;
        public int? cloud_type_id { get; set; } = 0;
        public string? cloud_type { get; set; } = string.Empty;
        public double? delta_t { get; set; } = 0;
        public int? gust_kmh { get; set; } = 0;
        public int? gust_kt { get; set; } = 0;
        public double? air_temp { get; set; } = 0;
        public double? dewpt { get; set; } = 0;    
        public double? press { get; set; } = 0;
        public double? press_qnh { get; set; } = 0;
        public double? press_msl { get; set; } = 0;
        public string press_tend { get; set; }  = string.Empty;
        public string? rain_trace { get; set; } = string.Empty;
        public int? rel_hum { get; set; } = 0;
        public string? sea_state { get; set; } = string.Empty;
        public string swell_dir_worded { get; set; } = string.Empty;
        public object swell_height { get; set; }
        public object swell_period { get; set; }
        public string vis_km { get; set; } = string.Empty;
        public string weather { get; set; } = string.Empty;
        public string wind_dir { get; set; } = string.Empty;
        public int? wind_spd_kmh { get; set; } = 0;
        public int? wind_spd_kt { get; set; } = 0;
    }
}