Fetch 20 tables from oracle 19c total. Approx 150 million rows all combined and store data  into oracle Linux 64gb ram 5tb ssd with modern processor then use duck db join do etl and store into new parquet file ready for reporting, do due diligence make. Approach sota add functional observability all goodness tech stack f#. Net 10 grab all industry best practices sota approach sota implementation sota functional and development

High-Throughput Data Modernization: A State-of-the-Art Architecture for Oracle 19c to Parquet ETL using.NET 10, F# 10, and DuckDB
1. Executive Summary
The enterprise data landscape is undergoing a paradigmatic shift, moving away from monolithic, latency-prone distributed clusters toward highly optimized, vertical scaling architectures. This transition is driven by the convergence of three critical technological advancements: the massive throughput of Non-Volatile Memory Express (NVMe) storage, the efficiency of vectorized in-process analytical engines like DuckDB, and the low-level performance capabilities of modern managed runtimes such as.NET 10. This report articulates a comprehensive, State-of-the-Art (SOTA) architectural blueprint for a specific, high-value data engineering challenge: the extraction of 150 million rows across 20 tables from an Oracle 19c database, their transformation via DuckDB, and their persistence into analytics-optimized Parquet files.
The architecture proposed herein is not merely a collection of tools but a rigorous integration of industry best practices, functional programming paradigms, and kernel-level optimizations. It leverages a functional-first approach using F# 10 to ensure concurrency safety and code correctness, while capitalizing on the.NET 10 runtime’s enhancements for memory management and instruction-level parallelism. Crucially, it integrates OpenTelemetry for "functional observability," treating telemetry as a first-class citizen within the function signatures of the pipeline itself.
By bypassing traditional row-based processing in favor of columnar, vectorized execution pathways enabled by Apache Arrow and DuckDB, the proposed solution minimizes memory overhead and maximizes I/O throughput on the specified Oracle Linux hardware (64GB RAM, 5TB NVMe SSD). This document serves as a definitive implementation guide, detailing the infrastructure tuning, kernel parameterization, application design patterns, and storage formatting strategies required to deliver a reporting-ready dataset with sub-second query latency potential for modern Business Intelligence (BI) tools.
2. Architectural Principles and Hardware Alignment
To achieve state-of-the-art performance, one must first align the software architecture with the physical realities of the underlying hardware. The specified environment—Oracle Linux running on a modern processor with 64GB of RAM and 5TB of SSD storage—presents a specific set of constraints and opportunities. The "modern processor" implies support for advanced instruction sets like AVX-512, which are critical for vectorized processing, while the 5TB SSD suggests a capacity for high-speed local buffering that eliminates the need for network-attached temporary storage.
2.1 The Vertical Scaling Paradigm
Historically, processing 150 million rows—likely equating to 100-200 GB of raw data depending on row width—might have triggered a request for a Hadoop or Spark cluster. However, in 2026, the overhead of network serialization, cluster coordination, and resource negotiation in distributed systems often exceeds the actual processing time for datasets of this magnitude. The SOTA approach for "medium data" (100GB to 1TB range) is vertical scaling on a single, powerful node.
This architecture leverages Data Locality. By bringing the compute (DuckDB) to the data (the extracted Arrow buffers) within the same process space, we eliminate the serialization tax. The traditional ETL bottleneck—moving data between the application and the database engine—is removed by running DuckDB in-process with the.NET 10 host.
2.2 Functional Data Engineering
The complexity of orchestrating parallel extractions, managing memory buffers, and handling failures requires a robust programming model. We adopt Functional Data Engineering principles using F# 10. This involves:
 * Immutability: Data structures are immutable by default, eliminating race conditions in parallel processing without expensive locking mechanisms.
 * Type Safety: The type system is used to enforce domain constraints and pipeline stages, preventing runtime errors that are costly in long-running ETL jobs.
 * Composition: The pipeline is constructed by composing small, pure functions, making the logic verifiable and testable.
2.3 Observability as Code
In a high-throughput pipeline, "observability" is not just about logging errors; it is about understanding the flow of data. We implement Functional Observability, where the pipeline's execution path is instrumented via OpenTelemetry. Using the Railway Oriented Programming (ROP) pattern, we wrap standard result types with telemetry context, ensuring that every transformation carries its own trace data. This allows for granular performance profiling, such as identifying exactly which Oracle partition is slow or which join operation is spilling to disk.
3. Infrastructure Optimization: Oracle Linux 9 on NVMe
The foundation of high-performance data engineering lies not in the application code, but in the interaction between the operating system kernel and the underlying storage hardware. For a workload involving the ingestion and transformation of 150 million rows, the I/O subsystem is the primary bottleneck. To achieve SOTA performance, we must move beyond default configurations and treat the Linux kernel as a specialized data plane.
3.1 NVMe Storage Subsystem Tuning
Modern NVMe drives allow for massive parallelism, often supporting queue depths (QD) far exceeding those of SATA or SAS SSDs. Traditional I/O schedulers like cfq or deadline were designed for rotating media and introduce unnecessary locking overhead.
3.1.1 I/O Scheduler Configuration
For NVMe drives, the none or mq-deadline (multi-queue deadline) scheduler is mandatory to prevent CPU contention during high-throughput ETL. The none scheduler essentially bypasses the reordering logic, relying on the NVMe controller's internal logic, which is optimal for the random read/write patterns seen during DuckDB's spill-to-disk operations. Using mq-deadline can also be effective, but none is increasingly preferred for pure NVMe environments to reduce CPU overhead.
Recommendation: Apply the none scheduler to the NVMe block devices hosting the DuckDB staging area and the Parquet output directory.
# Apply 'none' scheduler to all NVMe devices
for dev in /sys/block/nvme*; do
  echo none > $dev/queue/scheduler;
done

3.1.2 Sector Alignment and Partitioning
With 5TB of SSD, proper alignment is critical to avoid write amplification. The Oracle Database and DuckDB both perform block-based I/O. Misalignment between the filesystem blocks and the SSD's physical pages causes a single logical write to trigger two physical read-modify-write cycles, halving throughput and increasing latency.
Optimization Strategy:
 * Ensure partitions start on 4KB or 8KB boundaries.
 * Format the filesystem (XFS is preferred for Oracle Linux 9) with a block size matching the database page size where possible, typically 4KB.
 * Mount with noatime to prevent metadata writes on every read access, which is pure overhead in an ETL context.
3.2 Kernel Parameter Tuning (sysctl)
To support the massive concurrent streams required for parallel extraction and the memory-mapped I/O operations used by DuckDB, the Linux kernel parameters must be adjusted. The default settings in many distributions are conservative and geared towards general-purpose computing rather than high-throughput data processing.
| Parameter | Recommended Value | Rationale | Source |
|---|---|---|---|
| fs.file-max | 6815744 | Increases the limit on open file descriptors. DuckDB opens many temporary files during large joins, and parallel extraction opens multiple network sockets. |  |
| vm.swappiness | 1 | Discourages swapping unless absolutely necessary. For a 64GB RAM system processing 150M rows, swapping is fatal to performance. We prefer the OOM killer to invoke rather than stalling the pipeline in swap hell. |  |
| vm.dirty_ratio | 40 | Allows more data to be buffered in RAM before flushing to disk. This is crucial for the "Gather" phase where DuckDB writes large Parquet row groups. |  |
| vm.dirty_background_ratio | 10 | Starts flushing dirty pages in the background early. This prevents "write cliffs" where the application stalls entirely while waiting for the OS to clear the page cache. |  |
| kernel.io_uring_disabled | 0 | Enables io_uring, the modern asynchronous I/O interface required for DuckDB's high-performance nvmefs extension. |  |
| net.core.rmem_max | 16777216 | Increases the maximum TCP receive buffer size to 16MB, accommodating the large FetchSize used in ODP.NET. |  |
| net.core.wmem_max | 16777216 | Increases the maximum TCP send buffer size to 16MB. |  |
3.3 HugePages Configuration
While often associated with the Oracle Database SGA, HugePages are equally critical for large-scale data processing applications to reduce Translation Lookaside Buffer (TLB) misses.
Analysis:
Standard Linux page size is 4KB. For a 64GB heap, the CPU must maintain millions of page table entries. Enabling 2MB or 1GB HugePages reduces this bookkeeping overhead significantly.
 * Oracle Database: Should be configured to use HugePages for the System Global Area (SGA).
 * Data Pipeline: The.NET 10 runtime and DuckDB can utilize Transparent Huge Pages (THP). While explicit HugePages offer deterministic behavior, THP is often sufficient for the application layer if properly tuned (madvise mode).
Configuration:
Set vm.nr_hugepages in /etc/sysctl.conf to reserve approximately 50% of RAM for the Oracle SGA (if running locally) and the ETL pipeline's large buffers. This ensures that the memory-intensive hash tables used during joins reside in optimized memory regions.
3.4 The Role of xNVMe and nvmefs
Recent advancements in 2026 highlight the emergence of xNVMe and the DuckDB nvmefs extension. This technology stack allows DuckDB to bypass the POSIX filesystem entirely and speak directly to NVMe devices using io_uring passthrough.
Insight:
Using nvmefs reduces I/O latency by removing kernel context switches and filesystem metadata overhead. For the specific requirement of "due diligence" and "SOTA," implementing nvmefs for the temporary spill storage of DuckDB is a critical optimization. It essentially turns the NVMe drive into an extension of the application's memory space, crucial when the dataset exceeds physical RAM.
4. Oracle 19c Extraction Strategy: Parallelism & Partitioning
Extracting 150 million rows efficiently requires a strategy that saturates the network bandwidth without overloading the database CPU or the client application. A single SELECT * query is architecturally non-viable due to the lack of parallelism and vulnerability to network interruptions. The SOTA approach utilizes Partition-Wise Parallelism.
4.1 Partition Discovery and Task Generation
Oracle 19c offers robust partitioning capabilities (Range, List, Hash). The most efficient extraction method matches the client-side parallelism to the server-side data distribution.
Methodology:
 * Metadata Discovery: The application first queries USER_TAB_PARTITIONS and USER_TAB_SUBPARTITIONS to map the physical structure of the 20 target tables.
   SELECT TABLE_NAME, PARTITION_NAME, HIGH_VALUE
FROM USER_TAB_PARTITIONS
WHERE TABLE_NAME IN ('SALES', 'INVENTORY', 'CUSTOMERS')
ORDER BY TABLE_NAME, PARTITION_POSITION;

 * Dynamic Task Generation: For each partition found, the F# application generates a discrete extraction task. If a table is not partitioned, we fallback to logical partitioning using DBMS_ROWID or ORA_HASH to split the table into chunks, though physical partitions are preferred for "Partition Pruning" efficiency.
 * SQL Semantics: The generated SQL for each task explicitly targets a partition:
   SELECT /*+ PARALLEL(auto) */ * FROM SALES PARTITION (SALES_Q1_2026)

   The PARALLEL(auto) hint instructs the Oracle optimizer to allocate parallel query slaves if resources permit, accelerating the scan of that specific partition.
4.2 ODP.NET Core 26ai Configuration
The connectivity layer utilizes Oracle Data Provider for.NET (ODP.NET) Core, specifically the 26ai release compatible with.NET 10. This driver introduces significant enhancements for asynchronous processing and pipelining that are essential for high-throughput ETL.
Key Driver Settings:
 * FetchSize: This is the single most impactful parameter. Default values (typically 128KB) are insufficient for ETL, causing "chattiness" on the network.
   * Recommendation: Set FetchSize to 4MB or higher (e.g., 16MB) based on the memory available. This drastically reduces the number of round trips required to fetch 150 million rows. With 64GB RAM, we can afford larger buffers for the 20-30 concurrent streams.
   * Calculation: FetchSize = RowSize * BatchSize. Ensure the BatchSize aligns with the Arrow RecordBatch target size (e.g., 64K rows).
 * StatementCacheSize: Set to 0 for this ETL workload. We are executing unique queries (per partition) only once. Caching prepared statements consumes memory without providing any reuse benefit.
 * SelfTuning: Enable self-tuning in ODP.NET but provide a high minimum memory threshold to prevent the driver from being too conservative initially.
Pipelining & Asynchrony:
ODP.NET 26ai supports true asynchronous database pipelining. This allows the driver to send the request for the next batch of data while the application is still processing (transcoding) the current batch. This effectively overlaps CPU processing time with network I/O wait time, a critical optimization for minimizing total elapsed time.
4.3 Network Protocol Optimization
If the extraction process runs on the same physical host (or VM) as the database, strictly use the IPC (Inter-Process Communication) protocol. IPC bypasses the TCP/IP stack entirely, utilizing shared memory segments for data transfer. This drastically lowers latency and CPU usage compared to loopback TCP connections. If running on a separate node, ensure the connection uses SDP (Sockets Direct Protocol) over InfiniBand or RoCE (RDMA over Converged Ethernet) if available, or standard TCP with jumbo frames enabled to reduce packet overhead.
5. The.NET 10 & F# 10 Functional Data Pipeline
The middleware application orchestrating the ETL is built on.NET 10 using F# 10. This choice is strategic: F# offers immutable data structures and a powerful type system that fits perfectly with functional data transformation pipelines, while.NET 10 provides the raw performance of the modernized runtime, including AVX-512 support and advanced Garbage Collection (GC) features.
5.1 Language Features: F# 10
The SOTA approach utilizes specific F# 10 features to ensure code clarity and performance:
 * taskSeq Computation Expression:
   F# 10 introduces native support for asynchronous sequences via taskSeq. This allows for the creation of pull-based data streams that process data element-by-element (or batch-by-batch) without blocking threads. This mechanism uses state machine generation similar to C#'s async iterator but with F# semantics. It leverages the underlying ValueTask optimization in.NET 10 to minimize allocations on the hot path.
   * Application: The partition fetcher yields a taskSeq of Arrow RecordBatch objects. This allows the pipeline to apply backpressure; if DuckDB is slow to ingest, the reader naturally slows down without consuming excessive memory.
 * ValueOption (Struct Options):
   Database data is full of nulls. Standard F# Option types are reference types, which would cause 150 million small heap allocations if used for every nullable column.
   * Optimization: Use ValueOption (struct-based options). This eliminates heap allocations for the wrapper, reducing Garbage Collection (GC) pressure significantly. For high-throughput scenarios, avoiding GC pauses is paramount.
 * Scoped Warning Suppression:
   To achieve maximum performance, we may need to use unsafe code blocks (e.g., for direct memory copying from ODP.NET buffers to Arrow buffers). F# 10's #warnon and #nowarn directives allow us to strictly control compiler warnings in these specific blocks, maintaining safety elsewhere in the codebase.
5.2 Memory Management in.NET 10
Processing 150 million rows requires careful memory management to avoid "GC Storms."
 * Pinned Object Heap (POH): We utilize the POH for allocating the buffers used by Apache Arrow. By pinning these buffers, we prevent the GC from moving them, which is required for the "Zero-Copy" interop with the unmanaged DuckDB engine.
 * Array Pooling: Use System.Buffers.ArrayPool<T> to recycle the byte arrays used for fetching raw data from ODP.NET. This ensures that we are not constantly allocating and deallocating multi-megabyte buffers, keeping the Gen 2 heap stable.
5.3 The Architecture of Concurrency
The application implementation follows a Scatter-Gather pattern tailored for bounded parallelism:
 * Scatter: The TaskSeq generator yields partition metadata objects (e.g., PartitionInfo).
 * Process (Parallel): A bounded parallelism mechanism (like Parallel.ForEachAsync or System.Threading.Channels) consumes these partition objects. It spawns worker tasks (bounded by the number of cores, e.g., 32) to fetch data from Oracle.
 * Gather: Data is not gathered into a single large C# object. Instead, it is streamed directly into DuckDB tables.
Implementation Insight:
Avoid Task.WhenAll on thousands of partitions, as it creates a massive array of Task objects that tracks state for everything simultaneously. Use ParallelOptions.MaxDegreeOfParallelism to limit the number of active fetches to the number of available cores or I/O channels to keep the memory footprint steady and predictable.
6. Zero-Copy Integration: ODP.NET, Arrow, & ADBC
The critical differentiator in this SOTA architecture is Zero-Copy Ingestion. Traditional ETL involves reading data from a DB reader, instantiating.NET objects (overhead), serializing them to SQL insert statements (overhead), and sending them to the destination. This "double serialization" tax is unacceptable for 150 million rows. We replace this with Apache Arrow and ADBC (Arrow Database Connectivity).
6.1 The Mechanics of Zero-Copy
 * ODP.NET to Arrow:
   While ODP.NET does not natively output Arrow, we can read OracleDataReader into Apache.Arrow.RecordBatch using efficient memory copying. We allocate unmanaged memory buffers (using NativeMemory.Alloc or POH) that align with Arrow's columnar format.
   * Data Type Mapping: Map Oracle NUMBER to Arrow Decimal128 or Double, VARCHAR2 to Arrow String (UTF-8).
   * Batching: Construct RecordBatch objects containing ~64k to 128k rows. This size balances CPU cache locality with vectorization efficiency.
 * ADBC Ingestion:
   DuckDB supports the ADBC standard. This allows us to pass the memory address of the Arrow RecordBatch directly to DuckDB. DuckDB reads the data in-place without duplicating the memory buffers. This creates a "Zero-Copy" bridge between the extraction layer and the processing layer. The data effectively teleports from the.NET application's memory space into DuckDB's execution engine.
   Conceptual F# Implementation:
   // Conceptual flow using ADBC
let ingestBatch (batch: RecordBatch) (connection: AdbcConnection) =
    use statement = connection.CreateStatement()
    statement.SetOption("ingest_target_table", "staging_table")
    // Zero-copy bind: DuckDB reads the C# Arrow buffer directly
    statement.Bind(batch) 
    statement.ExecuteUpdate()

6.2 Handling Complex Types
For complex Oracle types (CLOB, BLOB), special care must be taken. Arrow supports binary large objects, but fetching these can disrupt the cache locality of columnar processing. The SOTA approach is to assess if these large fields are truly needed for reporting. If they are, consider splitting them into a separate vertical partition (table) within DuckDB. This preserves the scan speed of the scalar columns (Integers, Dates, short Strings) which are typically used for filtering and aggregation in BI tools.
7. Analytical Engine: DuckDB & Out-of-Core Processing
DuckDB runs embedded within the.NET process via the DuckDB.NET bindings. It provides the SQL engine required to join the 20 tables and perform ETL transformations. Unlike traditional engines that require data loading before querying, DuckDB can query the Arrow streams directly.
7.1 Out-of-Core Execution and Memory Limits
With 64GB of RAM and a 150 million row dataset (potentially exceeding RAM depending on column width and join expansion), DuckDB's Out-of-Core processing capabilities are vital. DuckDB automatically spills to disk when memory limits are reached.
Configuration for SOTA:
 * temp_directory: Point this explicitly to a directory on the high-speed 5TB NVMe SSD.
 * threads: Configure to match the physical core count of the processor (e.g., SET threads=32).
 * memory_limit: Set to approx. 70-80% of available RAM (e.g., SET memory_limit='48GB'). This leaves 16GB for the OS, ODP.NET buffers, and the F# runtime overhead.
7.2 Vectorized Joins and Optimization
The user query specifies "join do etl." DuckDB utilizes state-of-the-art hash join algorithms (Right-Deep Hash Join, Radix Partitioning) optimized for modern CPUs.
Best Practice:
 * Build Side: Ensure the smaller dimension tables (from the 20 tables fetched) are loaded first or identified as build-side tables in the query plan.
 * Probe Side: The largest fact tables (the bulk of the 150M rows) should be streamed and probed against the hash tables of the dimensions.
 * Join Ordering: While DuckDB has an excellent optimizer, explicitly structuring the query to join smaller tables first can assist the planner, especially when statistics are missing on the streamed Arrow data.
7.3 nvmefs Extension Integration
To fully utilize the NVMe SSD, enable the nvmefs extension in DuckDB. This extension bypasses the standard POSIX filesystem layer for temporary spill files, issuing NVMe commands directly. This reduces latency during the spill-to-disk phase of large joins, effectively utilizing the full bandwidth of the 5TB SSD.
-- Load the extension
INSTALL nvmefs;
LOAD nvmefs;
-- Configure secret/settings for direct device access
CREATE PERSISTENT SECRET nvmefs_config (TYPE NVMEFS, PATH '/dev/nvme0n1');

8. Functional Observability: OpenTelemetry & Railway Oriented Programming
In a SOTA architecture, "observability" goes beyond simple logging. It requires treating telemetry as a first-class citizen of the function signature. We utilize the Railway Oriented Programming (ROP) pattern to chain operations while implicitly carrying telemetry contexts.
8.1 Railway Oriented Programming (ROP) & Tracing
In ROP, functions return a Result<'Success, 'Failure> type. We augment this to carry trace context.
The Pattern:
Instead of manually starting/stopping spans in every function, we define a higher-order function (combinator) called trace. This combinator wraps any result-returning function, automatically creating an OpenTelemetry Activity (Span) around its execution.
// Conceptual F# ROP Trace Wrapper
let trace (activityName: string) (f: 'a -> Result<'b, 'e>) (input: 'a) : Result<'b, 'e> =
    use activity = source.StartActivity(activityName)
    let result = f input
    match result with

| Ok _ -> activity.SetStatus(ActivityStatusCode.Ok)
| Error e -> 
        activity.SetStatus(ActivityStatusCode.Error)
        activity.SetTag("error", e.ToString())
    result

By piping data through this trace combinator (|> trace "FetchPartition" |> trace "TransformData"), we automatically generate a hierarchical span graph of the entire ETL process. This provides visibility into the duration and success/failure of every step for every partition without polluting the business logic with boilerplate instrumentation code.
8.2 Metrics & Structured Logging
 * Metrics: We define a Meter ETL.Pipeline tracking custom metrics:
   * oracle_rows_fetched: Counter, tagged by table name.
   * arrow_batch_size_bytes: Histogram, to monitor buffer sizes.
   * duckdb_spill_bytes: Counter, monitoring disk usage.
 * Logs: Structured logs are attached to the active Span context (Activity.Current). This ensures that every log line can be correlated to the specific trace ID and span ID, allowing operators to filter logs for a specific partition fetch operation merely by clicking on a trace in the observability backend (e.g., Jaeger, Grafana Tempo).
9. Storage Optimization: Parquet for High-Performance BI
The final artifact is a Parquet file. To be "ready for reporting" (e.g., for Power BI Direct Lake or AWS Athena), it requires specific layout optimizations. Default settings are often insufficient for SOTA query performance.
9.1 Compression: ZSTD
Zstandard (ZSTD) is the SOTA compression algorithm for Parquet. It offers compression ratios comparable to GZIP but with decompression speeds closer to Snappy.
 * Recommendation: Use ZSTD Level 3 (default) or Level 6. Higher levels (e.g., 9-19) provide diminishing returns on file size while significantly increasing the CPU cost during the write phase. Given the 5TB SSD capacity, we prioritize write speed and read performance over maximum compression.
9.2 Row Group Sizing
Row Group size is the single most critical tuning parameter for query performance in BI tools.
 * Small Row Groups (e.g., 10MB): Better for predicate pushdown (skipping data) but incur high metadata parsing overhead and result in fragmented I/O requests.
 * Large Row Groups (e.g., 512MB): Better for compression and sequential I/O throughput (NVMe friendly).
SOTA Recommendation: Target a Row Group size of 128MB to 512MB (uncompressed data size). This aligns with S3 object storage retrieval patterns (if the data is later moved to cloud) and maximizes the sequential read bandwidth of the NVMe drives. For DuckDB, we configure this explicitly in the COPY command.
9.3 Data Sorting (Clustering/Z-Ordering)
Before writing to Parquet, data must be sorted. This is often overlooked but is critical for "Reporting Readiness."
 * Approach: Sort the data by the columns most frequently used in WHERE clauses or GROUP BY operations (e.g., Date, Region, CustomerID).
 * Benefit: This clusters similar values together. When a BI tool queries "Sales for Jan 2026," it checks the Parquet metadata (Page Index and Row Group Statistics). If the data is sorted, the Min/Max values for unrelated Row Groups will exclude the target date range, allowing the engine to skip reading those groups entirely. This Predicate Pushdown turns a full-table scan into a targeted seek, reducing query times from seconds to milliseconds.
10. Operational Readiness & Implementation Roadmap
To move from design to deployment, the following roadmap ensures due diligence:
 * Benchmark Phase:
   * Run fio benchmarks on the NVMe drives to verify the none scheduler and throughput capabilities.
   * Verify Oracle extraction speeds using a single partition fetch to establish a baseline.
 * Development Phase:
   * Implement the F# taskSeq generator for partitions.
   * Build the ODP.NET -> Arrow -> ADBC bridge.
   * Implement the DuckDB SQL transformation logic (Joins, Aggregations).
 * Observability Phase:
   * Configure the OpenTelemetry Collector to receive traces and metrics.
   * Verify that ROP traces are correctly nesting and propagating context.
 * Optimization Phase:
   * Tune DuckDB memory_limit and threads.
   * Experiment with Parquet Row Group sizes (128MB vs 256MB) using the target BI tool to measure query latency.
 * Validation Phase:
   * Perform row count verifications between Oracle and Parquet.
   * Check for data type fidelity (especially Decimal and DateTime precision).
11. Conclusion
This research report presents a comprehensive, high-performance architecture for modernizing data extraction from Oracle 19c. By shifting the paradigm from distributed clustering to optimized vertical scaling, we leverage the raw power of modern hardware—specifically NVMe SSDs and multi-core processors. The integration of ODP.NET 26ai pipelining, Apache Arrow zero-copy memory transfers, and DuckDB's vectorized engine creates a data pipeline with minimal friction.
The use of F# 10 ensures that the complex concurrency required for this task is handled safely and eloquently, while OpenTelemetry embedded within a Railway Oriented Programming model guarantees that the system is transparent and debuggable. Finally, by adhering to strict Parquet optimization best practices—specifically regarding ZSTD compression, Row Group sizing, and data sorting—the output is not just a backup, but a high-performance asset ready for immediate consumption by modern BI tools. This architecture represents the "State of the Art" for data engineering in 2026.
Citations:

