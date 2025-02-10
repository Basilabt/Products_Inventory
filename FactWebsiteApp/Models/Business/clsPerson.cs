using FactWebsiteApp.Models.DataAccess;

namespace FactWebsiteApp.Models.Business
{
    public class clsPerson
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public enum enGender
        {
            Male = 1 , Female = 2
        }

        public int personID { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string lastName { get; set; }

        public string fullname
        {
            get
            {
                return this.firstName + " " + this.secondName + " " + this.thirdName + " " + this.lastName; 
            }
        }

        public string genderAsString
        {
           get
           {
                return (this.gender == enGender.Male) ? "Male" : "Female";
           }
        }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public enGender gender { get; set; }

        public bool isMale
        {
            get
            {
                return this.gender == enGender.Male;
            }
        }

        public enMode mode { get; set; }

        public clsPerson()
        {
            this.personID = -1;
            this.firstName = "";
            this.secondName = "";
            this.thirdName = "";
            this.lastName = "";
            this.email = "";
            this.phoneNumber = "";
            this.gender = enGender.Male;
            this.mode = enMode.AddNew;
        }

        private clsPerson(int personID , string firstName , string seconName , string thirdName , string lastName , string email , string phoneNumber , enGender gender)
        {
            this.personID = personID;
            this.firstName = firstName;
            this.secondName = seconName;
            this.thirdName = thirdName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.gender = gender;
            this.mode = enMode.Update;

        }

        public bool save()
        {

            switch(this.mode)
            {
                case enMode.AddNew:
                    {
                        this.personID = addNewPerson(this.firstName,this.secondName,this.thirdName,this.lastName,this.email,this.phoneNumber,this.gender);
                        return this.personID != -1;
                    }

                case enMode.Update:
                    {
                        return updatePersonByPersonID(this.personID,this.firstName,this.secondName,this.thirdName,this.lastName,this.email,this.phoneNumber,this.gender);
                    }

                   case enMode.Delete:
                   {
                        return deletePersonByPersonID(this.personID);
                   }



            }


            return false;
        }

        public static int addNewPerson(string firstName, string seconName, string thirdName, string lastName, string email, string phoneNumber, enGender gender)
        {
            return clsPersonDataAccess.addNewPerson(firstName,seconName,thirdName,lastName,email,phoneNumber,gender == enGender.Male);
        }

        public static clsPerson getPersonByPersonID(int personID)
        {
            string firstName = "", secondName = "", thirdName = "", lastName = "", email = "", phoneNumber = "";
            bool gender = false;

            if(clsPersonDataAccess.getPersonByPersonID(personID,ref firstName,ref secondName,ref thirdName,ref lastName,ref email,ref phoneNumber,ref gender))
            {
                return new clsPerson(personID,firstName,secondName,thirdName,lastName,email,phoneNumber,(gender) ? enGender.Male :enGender.Female);
            }

            return null;
        }

        public static bool deletePersonByPersonID(int personID)
        {
            return clsPersonDataAccess.deletePersonByPersonID(personID);
        }

        public static bool updatePersonByPersonID(int personID , string firstName , string secondName , string thirdName , string lastName, string email , string phoneNumber, enGender gender)
        {
            return clsPersonDataAccess.updatePersonByPersonID(personID,firstName,secondName,thirdName,lastName,email,phoneNumber,(gender==enGender.Male));
        }
    }

}
