namespace AutotestingOnlineShops.Luma
{
    public class AddressData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

        public string FullAddress()
        {
            string fullAddress = $"{Firstname} {Lastname}"
                + "\r\n" + CompanyName
                + "\r\n" + StreetAddress
                + "\r\n" + $"{City}, {State}, {Zip}"
                + "\r\n" + Country
                + "\r\n" + $"T: {PhoneNumber}";
            return fullAddress;
        }

    }
}
