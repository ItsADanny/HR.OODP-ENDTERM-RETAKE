// Do not modify this class

class Applicant
{
    public string Name { get; set; }
    public int Age { get; set; }
    public TeachingCertificate Certificate { get; set; }
    public int YearsExperience { get; set; }

    public Applicant(string name, int age, TeachingCertificate certificate, int yearsExperience)
    {
        Name = name;
        Age = age;
        Certificate = certificate;
        YearsExperience = yearsExperience;
    }
}
