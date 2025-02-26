# Staff Management - Gym System

## **1. Staff Registration, Verification, and Login Process**

### **1.1 Staff Registration (End User Perspective)**
- A new staff member (e.g., trainer, receptionist) is registered by an admin.  The admin fills an online form with the new member's details.
- Required details:  
  - Full Name  
  - Email  
  - Phone Number  
  - Role Selection (Trainer, Receptionist, Manager, etc.)    
- The system sends a **verification email** with a unique activation link.  

### **1.2 Staff Verification**
- The staff clicks the activation link in the email.  
- They are directed to the login page and prompted to set a password as it was not created during registration.  
- Their account is now **active**.  

### **1.3 Staff Login**
- Staff enters their **email/username** and **password** into the login page.  
- Upon successful authentication, the system **generates a JWT token** (or a session token) and grants access based on their role.  

### **1.4 Viewing Own Data**
- Once logged in, staff can navigate to **"My Profile"** to view:  
  - Personal details (Name, Email, Phone)  
  - Payroll information (if applicable)  
  - Assigned tasks (for trainers, upcoming classes)  
- They can update **some details** (e.g., phone number, emergency contact) but not critical details like their role.  

---

## **2. Gym Staff Roles and Permissions**

| **Role**        | **Allowed Actions** | **Restricted Actions** |
|---------------|------------------|------------------|
| **Admin** | - Root User of the system : Register/Edit/Delete all staff members  <br> - Assign roles and register staff <br> - View all staff and member data <br> - Manage payments and memberships | Cannot book workout classes or change workout plans |
| **Trainer** | - View assigned clients and schedules <br> - Add workout plans <br> - Track client progress | Cannot manage payments or register new staff |
| **Receptionist** | - Register new members <br> - Check-in members for attendance <br> - View payment status | Cannot assign trainers or access payroll data |
| **Manager** | - Staff of the gym with the same rights as admin  <br> - Handle trainer registration  | Cannot directly edit member workout plans |

---

This document provides an overview of the staff management process in the gym system. It outlines how staff members register, verify their accounts, log in, and manage their profiles. Additionally, it details staff roles and their permissions within the system.
