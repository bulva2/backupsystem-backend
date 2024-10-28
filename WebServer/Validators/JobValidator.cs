using Quartz;
using System.Text.RegularExpressions;
using WebServer.Models;

namespace WebServer.Validators
{
    public class JobValidator : IValidator<Job>
    {
        public bool Validate(Job job)
        {
            if (job == null)
                return false;

            if (!ValidateRetentionCount(job.RetentionCount))
            {
                Console.WriteLine("Retention Count Validation failed!");
                return false;
            }

            if (!ValidateRetentionSize(job.RetentionSize))
            {
                Console.WriteLine("Retention Size Validation failed!");
                return false;
            }

            if (!ValidateTiming(job.Timing))
            {
                Console.WriteLine("Invalid CRON Timing! Validation failed.");
                return false;
            }

            if (!ValidateMethod(job.Method))
            {
                Console.WriteLine("Method Validation failed!");
                return false;
            }

            return true;
        }

        public bool ValidateRetentionCount(int count)
        {
            return count > 0;
        }

        public bool ValidateRetentionSize(int size)
        {
            return size > 0;
        }

        public bool ValidateTiming(string timing)
        {
            return CronExpression.IsValidExpression(timing);
        }

        public bool ValidateMethod(string method)
        {
            if (method == null)
                return false;

            List<string> validMethods = new List<string> { "full", "incremental", "differential" };
            return validMethods.Contains(method.ToLower());
        }
    }
}
