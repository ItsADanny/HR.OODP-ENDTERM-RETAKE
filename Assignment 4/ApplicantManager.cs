class ApplicantManager
{
    public List<Applicant> Applicants { get; private set; }
    
    // A Queue of type Applicant.
    public ... ToInvite { get; private set; }
    
     // A Dictionary with keys of type TeachingCertificate and values of type string.
    public static readonly ... CertificateInfo = new()
    {
        { TeachingCertificate.None, "Geen" },
        { TeachingCertificate.BDB1, "Basis Didactische Bekwaamheid 1" },
        { TeachingCertificate.BDB2, "Basis Didactische Bekwaamheid 2" },
        { TeachingCertificate.BKO, "Basis Kwalificatie Onderwijs" },
        { TeachingCertificate.SKO, "Senior Kwalificatie Onderwijs" },
    };

    public ApplicantManager(List<Applicant> applicants) => Applicants = applicants;

// Filtering applicants
    public void FilterApplicants(int minYears, TeachingCertificate minCertificate)
    {

    }

// Assembling and inviting applicants
    public void AssembleApplicantsByNameAndAge()
    {

    }

    public void InviteApplicants()
    {

    }
}
