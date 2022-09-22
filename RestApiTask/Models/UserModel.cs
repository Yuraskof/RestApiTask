namespace RestApiTask.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public Company company { get; set; }


        public class Address
        {
            public string street { get; set; }
            public string suite { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
            public Geo geo { get; set; }
        }

        public class Geo
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }

        public class Company
        {
            public string name { get; set; }
            public string catchPhrase { get; set; }
            public string bs { get; set; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            UserModel other = (UserModel)obj;

            if (id.Equals(other.id) && name.Equals(other.name) && username.Equals(other.username) &&
                email.Equals(other.email) && address.street.Equals(other.address.street) && address.city.Equals(other.address.city) && 
                address.suite.Equals(other.address.suite) && address.zipcode.Equals(other.address.zipcode) && 
                phone.Equals(other.phone) && website.Equals(other.website) && address.geo.lat.Equals(other.address.geo.lat) &&
                address.geo.lng.Equals(other.address.geo.lng) && company.bs.Equals(other.company.bs) &&
                company.catchPhrase.Equals(other.company.catchPhrase) && company.name.Equals(other.company.name))
            {
                Test.Log.Info("User models are equal");
                return true;
            }
            Test.Log.Error("User models aren't  equal");
            return false;
        }
    }
}
