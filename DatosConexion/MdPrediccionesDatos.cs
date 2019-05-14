using System;
using System.Collections.Generic;

namespace DatosConexion
{
    public class MdPrediccionesDatos
    {
        public string id { get; set; }
        public string project { get; set; }
        public string iteration { get; set; }
        public DateTime created { get; set; }
        public List<PredictionDatos> predictions { get; set; }

    }
    public class PredictionDatos
    {
        public double probability { get; set; }
        public string tagId { get; set; }
        public string tagName { get; set; }
        public BoundingBoxDatos boundingBox { get; set; }
    }

    public class BoundingBoxDatos
    {
        public double left { get; set; }
        public double top { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

}
