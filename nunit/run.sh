#!/bin/bash
if [ -d "/home/coder/project/workspace/CenterBookingSystem/" ]
then
    echo "project folder present"
    # checking for src folder
    if [ -d "/home/coder/project/workspace/CenterBookingSystem/" ]
    then
        cp -r /home/coder/project/workspace/nunit/test/TestProject /home/coder/project/workspace/;
        cp -r /home/coder/project/workspace/nunit/test/CenterBookingSystem.sln /home/coder/project/workspace/CenterBookingSystem/;
        cd /home/coder/project/workspace/CenterBookingSystem || exit;
        dotnet clean;
        dotnet test;
    else
        echo "CreateNewBooking_ValidDetails_JoinsSuccessfully_EventSpaceTable FAILED";
        echo "CreateNewBooking_ValidDetails_JoinsSuccessfully_BookingTable FAILED";
        echo "CreateNewBooking_ValidDetails_JoinsSuccessfully_BookingTable1 FAILED";
        echo "AvailableSpaces_Method_Exists_EventSpaceController FAILED";
        echo "Index_Method_Exists_BookingController_parameter FAILED";
        echo "Create_Method_Exists_BookingController_4_parameter FAILED";
        echo "Confirmation_Method_Exists_BookingController_1_parameter FAILED";
        echo "Confirmation_InvaalidID_ReturnsNotFoundResult FAILED";
        echo "Confirmation_ValidID_Returns_ViewResule_Booking FAILED";
        echo "EventSpace_ClassExists FAILED";
        echo "Booking_ClassExists FAILED";
        echo "EventBookingException_ClassExists FAILED";
        echo "BookingDbContextContainsDbSetEventSpacesProperty FAILED";
        echo "BookingDbContextContainsDbSetBookingsProperty FAILED";
        echo "EventSpace_Properties_SpaceID_ReturnExpectedDataTypes_int FAILED";
        echo "EventSpaces_Properties_Name_ReturnExpectedDataTypes_string FAILED";
        echo "EventSpaces_Properties_Capacity_ReturnExpectedDataTypes_DateTime FAILED";
        echo "EventSpaces_Properties_Availability_ReturnExpectedDataTypes_Bool FAILED";
        echo "EventSpaces_Properties_Bookings_ReturnExpectedDataTypes_ICollection_Booking FAILED";
        echo "Booking_Properties_SpaceID_ReturnExpectedDataTypes_int FAILED";
        echo "Booking_Properties_TimeSlot_ReturnExpectedDataTypes_TimeSpan FAILED";
        echo "Booking_Properties_EventDate_ReturnExpectedDataTypes_DateTime FAILED";
        echo "Test_ConfirmationViewFile_Exists_Booking FAILED";
        echo "Test_CreateViewFile_Exists_Booking FAILED";
        echo "Test_IndexViewFile_Exists_Booking FAILED";
        echo "Test_AvailableViewFile_Exists_EvantSpace FAILED";
    fi
else
    echo "CreateNewBooking_ValidDetails_JoinsSuccessfully_EventSpaceTable FAILED";
    echo "CreateNewBooking_ValidDetails_JoinsSuccessfully_BookingTable FAILED";
    echo "CreateNewBooking_ValidDetails_JoinsSuccessfully_BookingTable1 FAILED";
    echo "AvailableSpaces_Method_Exists_EventSpaceController FAILED";
    echo "Index_Method_Exists_BookingController_parameter FAILED";
    echo "Create_Method_Exists_BookingController_4_parameter FAILED";
    echo "Confirmation_Method_Exists_BookingController_1_parameter FAILED";
    echo "Confirmation_InvaalidID_ReturnsNotFoundResult FAILED";
    echo "Confirmation_ValidID_Returns_ViewResule_Booking FAILED";
    echo "EventSpace_ClassExists FAILED";
    echo "Booking_ClassExists FAILED";
    echo "EventBookingException_ClassExists FAILED";
    echo "BookingDbContextContainsDbSetEventSpacesProperty FAILED";
    echo "BookingDbContextContainsDbSetBookingsProperty FAILED";
    echo "EventSpace_Properties_SpaceID_ReturnExpectedDataTypes_int FAILED";
    echo "EventSpaces_Properties_Name_ReturnExpectedDataTypes_string FAILED";
    echo "EventSpaces_Properties_Capacity_ReturnExpectedDataTypes_DateTime FAILED";
    echo "EventSpaces_Properties_Availability_ReturnExpectedDataTypes_Bool FAILED";
    echo "EventSpaces_Properties_Bookings_ReturnExpectedDataTypes_ICollection_Booking FAILED";
    echo "Booking_Properties_SpaceID_ReturnExpectedDataTypes_int FAILED";
    echo "Booking_Properties_TimeSlot_ReturnExpectedDataTypes_TimeSpan FAILED";
    echo "Booking_Properties_EventDate_ReturnExpectedDataTypes_DateTime FAILED";
    echo "Test_ConfirmationViewFile_Exists_Booking FAILED";
    echo "Test_CreateViewFile_Exists_Booking FAILED";
    echo "Test_IndexViewFile_Exists_Booking FAILED";
    echo "Test_AvailableViewFile_Exists_EvantSpace FAILED";
fi
    