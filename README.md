# jcDB
A lightweight .NET Core 3.0 Key/Value Database with support for persistence and random writes

Current 0.0.3 release benchmarks:

| Seconds in Test  |   Objects     |   Throughput  |
| ---------------- |---------------|:-------------:|
| 3                | 1693299        | 564433/s      |
| 5                | 2987505        | 597501/s      |
| 10               | 6414610        | 641461/s      |
| 15               | 8593065        | 572871/s      |
| 20               | 12157720       | 607886/s      |

Benchmarks were performed on an Intel i7-7700K in Release Mode
