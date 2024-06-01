using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Reports_TPD_Lab_6
{
    class StationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Title { get; set; }
        public string MeasuredParameters { get; set; }
        public string MeasurmentTime { get; set; }

        private int _id;

        private string _name, _designation, _title, _measuredparameters;

        private DateTime _measurmentTime;

        public StationModel(StationModel stationModel)
        {
            _id = stationModel.Id;
            _name = stationModel.Name;
            _designation = stationModel.Designation;
            _title = stationModel.Title;
            _measuredparameters = stationModel.MeasuredParameters;
            _measurmentTime = DateTime.Parse(stationModel.MeasurmentTime);

        }
    }
}
