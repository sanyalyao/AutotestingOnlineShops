using System;

namespace AutotestingOnlineShops.Luma
{
    public class AccountData : IEquatable<AccountData>, IComparable<AccountData>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            return $"FirstName = {FirstName}; LastName = {LastName}; Email = {Email}; Password = {Password}";
        }

        public int CompareTo(AccountData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (LastName.CompareTo(other.LastName) == 0)
            {
                FirstName.CompareTo(other.FirstName);
            }
            return LastName.CompareTo(other.LastName);
        }

        public bool Equals(AccountData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, null))
            {
                return true;
            }
            return FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName);
        }

    }
}
