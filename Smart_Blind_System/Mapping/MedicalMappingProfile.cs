using AutoMapper;
using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Entities.MedicalEntity;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos;
using Smart_Blind_System.API.DTOs.MedicalProfileDtos.ChronicDiseaseiesDtos;

namespace Smart_Blind_System.API.Mapping
{
    public class MedicalMappingProfile : Profile
    {
        public MedicalMappingProfile()
        {
            // ===== MedicalProfile =====
            CreateMap<CreateMedicalProfileDto, MedicalProfile>()
                .ForMember(d => d.Notes, o => o.MapFrom(s => s.MedicalNotes))
                .ForMember(d => d.DrPhone, o => o.MapFrom(s => s.DoctorPhone));

            CreateMap<UpdateMedicalProfileDto, MedicalProfile>();

            CreateMap<MedicalProfile, MedicalProfileDto>()
                .ForMember(d => d.MedicalNotes, o => o.MapFrom(s => s.Notes))
                .ForMember(d => d.DoctorPhone, o => o.MapFrom(s => s.DrPhone))
                .ForMember(d => d.BloodType, o => o.MapFrom(s => s.BloodType.ToString()))
                .ForMember(d => d.MedicalHistory, o => o.MapFrom(s => s.HistoryEntries));

            // ===== Allergy =====
            CreateMap<CreateAllergyDto, Allergy>();
            CreateMap<UpdateAllergyDto, Allergy>();
            CreateMap<Allergy, AllergyDto>();

            // ===== ChronicDisease =====
            CreateMap<CreateChronicDiseaseDto, ChronicDisease>();
            CreateMap<UpdateChronicDiseaseDto, ChronicDisease>();
            CreateMap<ChronicDisease, ChronicDiseaseDto>();

            // ===== MedicalHistoryEntry =====
            CreateMap<CreateMedicalHistoryEntryDto, MedicalHistoryEntry>();
            CreateMap<UpdateMedicalHistoryEntryDto, MedicalHistoryEntry>();
            CreateMap<MedicalHistoryEntry, MedicalHistoryEntryDto>();

            // ===== Medication =====
            CreateMap<CreateMedicationDto, Medication>();
            CreateMap<Medication, MedicationDto>();
        }
    }
}