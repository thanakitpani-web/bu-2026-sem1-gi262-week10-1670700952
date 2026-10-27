using System;
using System.Collections.Generic;

namespace MidtermExam.Prob02
{
    /// <summary>
    /// ข้อ 02: จำลองประวัติการเปิดเว็บด้วย Stack สองชุด ตามหลัก LIFO
    /// (สมาชิกที่ใส่ล่าสุดถูกนำออกก่อน) นักศึกษาทำเฉพาะเมธอดในคลาสนี้
    /// ส่วนแยกคำสั่งจากข้อความเตรียมไว้แล้วใน BrowserNavigation.cs
    /// </summary>
    public class StudentWebBrowser
    {
        // ประวัติหน้าที่สามารถย้อนกลับได้: หน้าบนสุดคือหน้าที่ Back() จะเปิดถัดไป
        private Stack<string> backHistory = new Stack<string>();
        // ประวัติหน้าที่สามารถไปข้างหน้าได้: หน้าบนสุดคือหน้าที่ Forward() จะเปิดถัดไป
        private Stack<string> forwardHistory = new Stack<string>();
        // หน้าที่กำลังเปิดอยู่ แยกจากทั้งสอง Stack; ข้อความว่างหมายถึงยังไม่เคยเปิดหน้าเว็บ
        private string currentUrl = string.Empty;

        /// <summary>เปิดหน้าเว็บใหม่ และปรับประวัติสำหรับการย้อนกลับและไปข้างหน้า</summary>
        /// <param name="url">
        /// URL ที่ต้องการเปิด ถ้าเป็น null, ข้อความว่าง หรือช่องว่างล้วน ให้ข้ามโดยไม่เปลี่ยนสถานะใด ๆ
        /// สำหรับ URL ที่ใช้ได้ ให้เก็บข้อความตามที่ได้รับ ไม่ต้องตัดช่องว่างในเมธอดนี้
        /// </param>
        /// <remarks>
        /// ถ้ามีหน้าปัจจุบันอยู่แล้ว ต้องเก็บหน้านั้นเป็นประวัติย้อนกลับ
        /// การเปิด URL ใหม่ต้องล้างประวัติไปข้างหน้า (redo stack) แม้ URL ใหม่จะตรงกับหน้าปัจจุบัน
        /// ตัวอย่าง: เปิด a, เปิด b, ย้อนกลับ, เปิด c แล้วสั่งไปข้างหน้า ต้องยังอยู่ที่ c
        /// </remarks>
        public void Open(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            if (!string.IsNullOrEmpty(currentUrl))
                backHistory.Push(currentUrl);

            currentUrl = url;
            forwardHistory.Clear();
        }

        /// <summary>ย้อนกลับไปยังหน้าล่าสุดในประวัติย้อนกลับหนึ่งขั้น</summary>
        /// <remarks>
        /// ถ้า backHistory ว่าง ให้จบเมธอดโดยไม่เปลี่ยนสถานะ และต้องไม่เกิด exception
        /// หน้าที่ออกจากการดูจะต้องเก็บไว้ เพื่อให้ Forward() กลับมาหน้านั้นได้
        /// </remarks>
        public void Back()
        {
            if (backHistory.Count == 0)
                return;

            forwardHistory.Push(currentUrl);
            currentUrl = backHistory.Pop();
        }

        /// <summary>ไปข้างหน้าไปยังหน้าล่าสุดในประวัติไปข้างหน้าหนึ่งขั้น</summary>
        /// <remarks>
        /// ถ้า forwardHistory ว่าง ให้จบเมธอดโดยไม่เปลี่ยนสถานะ และต้องไม่เกิด exception
        /// หน้าที่ออกจากการดูจะต้องเก็บไว้ เพื่อให้ Back() กลับมาหน้านั้นได้
        /// </remarks>
        public void Forward()
        {
            if (forwardHistory.Count == 0)
                return;

            backHistory.Push(currentUrl);
            currentUrl = forwardHistory.Pop();
        }

        /// <summary>อ่าน URL ของหน้าปัจจุบัน โดยไม่เปลี่ยนหน้าหรือประวัติใด ๆ</summary>
        /// <returns>URL ปัจจุบัน หรือ string.Empty ถ้ายังไม่เคยเปิดหน้าเว็บ</returns>
        public string Current()
        {
            return currentUrl;
        }
    }
}
