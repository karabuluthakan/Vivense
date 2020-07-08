using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.ApacheKafka.Events.Product.Infos
{
    public class ProductPackageInfo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int No { get; set; }
        public int WeightKg { get; set; }
        public double VolumeCm3 { get; set; }

        /// <summary>
        ///     Create intial Id for this package
        /// </summary>
        public ProductPackageInfo()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public double Desi() => VolumeCm3 / 3000;

    }
}