﻿using System.Threading.Tasks;
using Library.Utilities.Results.Abstract;

namespace Library.CrossCuttingConcerns.HealthChecks.Abstract
{
    public interface IKafkaHealthChecker
    {
        Task<IResult> GetKafkaStatus();
    }
}