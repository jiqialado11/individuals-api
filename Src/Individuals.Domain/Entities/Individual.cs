using System;
using System.Collections.Generic;
using Individuals.Domain.Enums;

namespace Individuals.Domain.Entities
{
    public class Individual:Entity
    {
        #region Constructors

        protected Individual()
        {
            PhoneNumbers = new List<PhoneNumber>();
            ConnectedIndividuals=new List<ConnectedIndividual>();
        }
        public Individual(string firstName, string lastName, GenderType gender,
                          string personalNumber, DateTime dateOfBirth, 
                          City city)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalNumber = personalNumber;
            DateOfBirth = dateOfBirth;
            City = city;
            PhoneNumbers = new List<PhoneNumber>();
            ConnectedIndividuals = new List<ConnectedIndividual>();
        }
        #endregion

        #region Properties

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public GenderType? Gender { get; protected set; }
        public string PersonalNumber { get; protected set; }
        public DateTime? DateOfBirth { get; protected set; }
        public City City { get; protected set; }
        public string ImagePath { get;protected set; }
        
        public List<PhoneNumber> PhoneNumbers { get; protected set; }
        public List<ConnectedIndividual> ConnectedIndividuals { get;protected set; }

        #endregion

        #region Methods

        public void AddPhoneNumber(PhoneNumber number)
        {
            PhoneNumbers.Add(number);
        }

        public void UpdateIndividual(string firstName, string lastName, GenderType? gender, string personalId,
            DateTime? dateOfBirth, City city)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalNumber = personalId;
            DateOfBirth = dateOfBirth;
            City = city;
        }

        public void SetImage(string imagePath)
        {
            ImagePath = imagePath;
        }

        public void RemoveImage()
        {
            ImagePath = string.Empty;
        }

        public void SetConnection(ConnectedIndividual individualConnection)
        {
            ConnectedIndividuals.Add(individualConnection);
        }
        #endregion
    }
}
