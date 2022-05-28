namespace REST_API.Models
{
    public class Member
    {
        // Anchor for the eventHandler
        public event EventHandler<PasswordEventAgrs> PasswordEvent;


        private string _password { get; set; }

        public Member()
        {
            Name = "name";
            _listPhoneNo = new List<string>();
        }

        public Int32 MemberID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password
        {
            get
            {
                CheckOnPasswordChangeDate();
                return _password;
            }
            set
            {
                _password = value;
                dateCreated();
                changeDate();
            }
        }

        public List<string> _listPhoneNo;
        public string PhoneNumber
        {
            get { return GetPhoneNumInString(); }
            set
            {
                if (value.IndexOf(',') > -1)
                {
                    DeserializePhoneNumber(value);
                }
                else
                {
                    AddPhoneNumber(value);
                }
            }
        }

        private void AddPhoneNumber(string phNo)
        {
            _listPhoneNo.Add(phNo);
        }

        private void DeserializePhoneNumber(string numberInString)
        {
            _listPhoneNo = numberInString.Split(',').ToList();

            for (int i = 0; i < _listPhoneNo.Count; i++)
            {
                _listPhoneNo[i] = _listPhoneNo[i].Trim();
            }
        }

        private string GetPhoneNumInString()
        {
            return string.Join(", ", _listPhoneNo);
        }

        // save the date created
        public DateTime createMemberDate { get; private set; }

        private void dateCreated()
        {
            createMemberDate = DateTime.Now;
        }

        // After create the membership --> set when is the next date for the user to change the password
        public DateTime passwordChangeDate { get; private set; }

        private void changeDate()
        {
            passwordChangeDate = createMemberDate.AddDays(7);
        }


        private void CheckOnPasswordChangeDate()
        {
            if (this.passwordChangeDate >= DateTime.Now)
            {
                PasswordEventAgrs args = new PasswordEventAgrs();
                args.MessageEvent = $"Hi {this.Name}! Your password is expired, please change to new password";
                args.Category = "changePassword";
                OnChangePasswordDate(args);
            }

        }

        protected virtual void OnChangePasswordDate(PasswordEventAgrs PasswordArg)
        {
            EventHandler<PasswordEventAgrs> handle = PasswordEvent;

            if (handle != null)
            {
                handle(this, PasswordArg);
            }
        }
    }

    public class PasswordEventAgrs
    {
        public string MessageEvent { get; set; }
        public string Category { get; set; }
    }
}
