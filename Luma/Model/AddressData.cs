using System;

namespace AutotestingOnlineShops.Luma
{
    public class AddressData : IEquatable<AddressData>, IComparable<AddressData>
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

        public override string ToString()
        {
            return $"Phone = {PhoneNumber}; Street Address = {StreetAddress}; City = {City}; Zip = {Zip}";
        }

        public int CompareTo(AddressData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return StreetAddress.CompareTo(other.StreetAddress);
        }

        public bool Equals(AddressData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, null))
            {
                return true;
            }
            return StreetAddress.Equals(other.StreetAddress) && City.Equals(other.City) && Zip.Equals(other.Zip) && Country.Equals(other.Country);
        }

        public string FullDefaultAddress()
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
