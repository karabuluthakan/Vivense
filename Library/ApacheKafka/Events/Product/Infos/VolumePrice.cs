namespace Library.ApacheKafka.Events.Product.Infos
{
    /// <summary>
    ///     Samples
    ///    1 --> 5
    ///    2 --> 4.5
    ///    3 --> 4
    ///    
    ///     Buy: 6 --> 6*4 = 24;
    /// 
    ///    1 --> 10
    ///    5 --> 9
    ///    20 --> 7
    ///    
    ///     Buy: 10 --> 10 * 9 = 90
    /// </summary>
    public class VolumePrice
    {
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
     
    }
}
