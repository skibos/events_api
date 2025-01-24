using Events.Domain.Events;
using Events.Domain.Events.Enums;
using Events.Domain.Events.ValueObjects;
using Events.Domain.Users;
using Events.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Persistance.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        ConfigureEventsTable(builder);
        ConfigureParticipantsTable(builder);
    }

    private void ConfigureEventsTable(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

        builder.HasKey(e => e.Id);
        builder.Property(u => u.Id)
            .HasColumnName("EventId")
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => EventId.Create(value)
            );

        builder.OwnsOne(e => e.Location, location =>
        {
            location.Property(l => l.Latitude).HasColumnName("Latitude");
            location.Property(l => l.Longitude).HasColumnName("Longitude");
        });


        builder.Property(e => e.EventStatus).HasConversion(
            enumeration => enumeration.ToString(),
            dbValue => (EventStatus)Enum.Parse(typeof(EventStatus), dbValue)
        );
    }

    private void ConfigureParticipantsTable(EntityTypeBuilder<Event> builder)
    {
        builder.OwnsMany(e => e.Participants, participantsBuilder => {
            participantsBuilder.ToTable("participants");

            participantsBuilder
                .WithOwner()
                .HasForeignKey("EventId");

            participantsBuilder.HasKey(p => p.Id);

            participantsBuilder.Property(p => p.Id)
                .HasColumnName("ParticipantId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ParticipantId.Create(value)
                );

            participantsBuilder
                .Property(m => m.UserId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));

            participantsBuilder.HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);


            participantsBuilder.Property(p => p.Role).HasConversion(
                enumeration => enumeration.ToString(),
                dbValue => (ParticipantRole)Enum.Parse(typeof(ParticipantRole), dbValue)
            );

            participantsBuilder.Property(p => p.AttendanceStatus).HasConversion(
                enumeration => enumeration.ToString(),
                dbValue => (AttendanceStatus)Enum.Parse(typeof(AttendanceStatus), dbValue)
            );
        });
    }
}