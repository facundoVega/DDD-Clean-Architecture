using CleanArchitecture.Applications.Abstractions.Messaging;
using QuestPDF.Fluent;

namespace CleanArchitecture.Application.Vehicles.ReportVehiclePdf;

public sealed record ReportVehiclePdfQuery(string Model) :IQuery<Document>;
    