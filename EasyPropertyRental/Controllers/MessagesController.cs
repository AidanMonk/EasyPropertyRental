using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyPropertyRental.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Buffers.Text;
using System.Security.Claims;
using EasyPropertyRental.Models.ViewModels;

namespace EasyPropertyRental.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly PropertyRentalDbContext _context;

        public MessagesController(PropertyRentalDbContext context, ILogger<MessagesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var propertyRentalDbContext = _context.Messages.Include(m => m.Receiver).Include(m => m.Receiver1).Include(m => m.ReceiverNavigation).Include(m => m.Sender).Include(m => m.Sender1).Include(m => m.SenderNavigation);
            return View(await propertyRentalDbContext.ToListAsync());
        }
        // GET: Messages/ChooseAction
        public IActionResult ChooseAction()
        {
            return View();
        }

        // GET: Messages/SelectReceiverType
        public IActionResult SelectReceiverType()
        {
            var receiverTypes = new List<string> { "PropertyManager", "Tenant", "PropertyOwner" };
            return View(receiverTypes);
        }

        // POST: Messages/SelectReceiverType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectReceiverType(string receiverType)
        {
            TempData["SelectedReceiverType"] = receiverType;
            return RedirectToAction("Send");
        }


        // GET: Messages/Send
        public async Task<IActionResult> Send()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            int loggedInUserId = 0;
            string loggedInUserType = "";

            if (userRole == "Tenant")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "t_id")?.Value, out loggedInUserId);
                loggedInUserType = "Tenant";
            }
            else if (userRole == "PropertyManager")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out loggedInUserId);
                loggedInUserType = "PropertyManager";
            }
            else if (userRole == "PropertyOwner")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out loggedInUserId);
                loggedInUserType = "PropertyOwner";
            }

            if (string.IsNullOrEmpty(loggedInUserType) || loggedInUserId == 0)
            {
                return RedirectToAction("Error"); // Handle missing user information
            }

            // Example data for receivers (replace with database query)
            var receivers = await _context.PropertyManagers
                .Select(pm => new ReceiverDto { Id = pm.PmId, Name = pm.FirstName })
                .ToListAsync();

            ViewData["SenderType"] = loggedInUserType;
            ViewData["SenderId"] = loggedInUserId;
            ViewData["ReceiverType"] = "PropertyManager"; // Set based on logic or selection
            ViewData["ReceiverList"] = receivers;

            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            // Log the message object for debugging purposes
            _logger.LogInformation("Received message: {MessageContent}, SenderId: {SenderId}, ReceiverId: {ReceiverId}, SentAt: {SentAt}",
                message.Content, message.SenderId, message.ReceiverId, message.SentAt);

            // Get the user role and details
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            int loggedInUserId = 0;
            string loggedInUserType = "";

            if (userRole == "Tenant")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "t_id")?.Value, out loggedInUserId);
                loggedInUserType = "Tenant";
            }
            else if (userRole == "PropertyManager")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out loggedInUserId);
                loggedInUserType = "PropertyManager";
            }
            else if (userRole == "PropertyOwner")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out loggedInUserId);
                loggedInUserType = "PropertyOwner";
            }

            // Set the sender's information
            message.SenderId = loggedInUserId;
            message.SenderType = loggedInUserType;
            message.SentAt = DateTime.Now;

            // Assuming you are getting receiver information from the form or another source
            message.ReceiverId = message.ReceiverId; // ReceiverId will come from form input
            message.ReceiverType = message.ReceiverType; // ReceiverType will come from form input

            // Log the updated message before saving
            _logger.LogInformation("Saving message: {MessageContent}, SenderId: {SenderId}, ReceiverId: {ReceiverId}, SentAt: {SentAt}",
                message.Content, message.SenderId, message.ReceiverId, message.SentAt);

            // Save the message into the database
            _context.Messages.Add(message);
            _context.SaveChanges();

            // Redirect to a confirmation or messages list view after saving
            return RedirectToAction("Index", "Messages"); // Adjust accordingly
        }



        private (string UserType, int UserId) GetLoggedInUserInfo()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            int userId = 0;
            string userType = string.Empty;

            if (userRole == "Tenant")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "t_id")?.Value, out userId);
                userType = "Tenant";
            }
            else if (userRole == "PropertyManager")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out userId);
                userType = "PropertyManager";
            }
            else if (userRole == "PropertyOwner")
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out userId);
                userType = "PropertyOwner";
            }

            return (userType, userId);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Receiver1)
                .Include(m => m.ReceiverNavigation)
                .Include(m => m.Sender)
                .Include(m => m.Sender1)
                .Include(m => m.SenderNavigation)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["ReceiverId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId");
            ViewData["ReceiverId"] = new SelectList(_context.Tenants, "TenantId", "TenantId");
            ViewData["ReceiverId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId");
            ViewData["SenderId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId");
            ViewData["SenderId"] = new SelectList(_context.Tenants, "TenantId", "TenantId");
            ViewData["SenderId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,SenderType,SenderId,ReceiverType,ReceiverId,Content,SentAt")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceiverId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", message.ReceiverId);
            ViewData["ReceiverId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", message.ReceiverId);
            ViewData["ReceiverId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", message.SenderId);
            ViewData["SenderId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", message.SenderId);
            ViewData["SenderId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", message.SenderId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["ReceiverId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", message.ReceiverId);
            ViewData["ReceiverId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", message.ReceiverId);
            ViewData["ReceiverId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", message.SenderId);
            ViewData["SenderId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", message.SenderId);
            ViewData["SenderId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", message.SenderId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,SenderType,SenderId,ReceiverType,ReceiverId,Content,SentAt")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceiverId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", message.ReceiverId);
            ViewData["ReceiverId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", message.ReceiverId);
            ViewData["ReceiverId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", message.SenderId);
            ViewData["SenderId"] = new SelectList(_context.Tenants, "TenantId", "TenantId", message.SenderId);
            ViewData["SenderId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", message.SenderId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Receiver1)
                .Include(m => m.ReceiverNavigation)
                .Include(m => m.Sender)
                .Include(m => m.Sender1)
                .Include(m => m.SenderNavigation)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
