

public class CpuFactory
{
    public static ICpuCore GetRandomCpu(){
        return new CpuRandom();
    }
}