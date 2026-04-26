namespace Project;

public static class Admission {
    private static double _passingScore = 4.5;
    public static double PassingScore {
        get { return _passingScore; }
        set { _passingScore = value; }
    }
    public static bool IsPassingScore(Applicant applicant) {
        return applicant.AverageGrade >= _passingScore;
    }
}
