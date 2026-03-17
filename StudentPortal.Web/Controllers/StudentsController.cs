using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;
using StudentPortal.Web.Configurations;
using StudentPortal.Web.Dtos;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext _context { get; }
        private readonly IMapper _mapper;

        public StudentsController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var student = new Student()
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            List<Student> students = await _context.Students.OrderBy(q => q.Name).ToListAsync();

            List<StudentDto> studentsDto = _mapper.Map<List<StudentDto>>(students);

            return View("List", studentsDto);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _context.Students.OrderBy(q => q.Name).ToListAsync();

            List<StudentDto> studentsDto = _mapper.Map<List<StudentDto>>(students);

            return View(studentsDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid studentId)
        {
            EditStudentViewModel viewModel = new EditStudentViewModel();

            try
            {
                var currentStudent = await _context.Students.Where(q => q.Id == studentId).FirstOrDefaultAsync();

                if (currentStudent == null)
                    throw new Exception("Student not found");

                viewModel = _mapper.Map<EditStudentViewModel>(currentStudent);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var currentStudent = await _context.Students.Where(q => q.Id == viewModel.Id).FirstOrDefaultAsync();

            if (currentStudent != null)
            {
                currentStudent.Name = viewModel.Name;
                currentStudent.Email = viewModel.Email;
                currentStudent.Phone = viewModel.Phone;
                currentStudent.Subscribed = viewModel.Subscribed;

                await _context.SaveChangesAsync();
            }

            var students = await _context.Students.OrderBy(q => q.Name).ToListAsync();

            List<StudentDto> studentsDto = _mapper.Map<List<StudentDto>>(students);

            return View("List", studentsDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid studentId)
        {
            var currentStudent = await _context.Students.Where(q => q.Id == studentId).FirstOrDefaultAsync();

            if (currentStudent != null)
            {
                _context.Students.Attach(currentStudent);
                _context.Students.Remove(currentStudent);

                await _context.SaveChangesAsync();
            }

            var students = await _context.Students.OrderBy(q => q.Name).ToListAsync();

            List<StudentDto> studentsDto = _mapper.Map<List<StudentDto>>(students);

            return View("List", studentsDto);
        }
    }
}
