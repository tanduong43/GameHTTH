const nodemailer = require('nodemailer');

// Setup Nodemailer transporter with Gmail
const transporter = nodemailer.createTransport({
    service: 'gmail',
    auth: {
        user: process.env.EMAIL_USER || 'vtan69290@gmail.com',
        pass: process.env.EMAIL_PASS || 'lubs xjkr gqkb ekmf'
    }
});

/**
 * Sends an email notification to the admin when a new deposit request is created.
 * @param {string} username - The username of the user making the deposit.
 * @param {number} amount - The amount being deposited.
 * @param {string} transferContent - The expected bank transfer syntax/content.
 * @param {string} code - The generated deposit request code.
 */
async function sendAdminDepositNotification(username, amount, transferContent, code) {
    const adminEmail = process.env.EMAIL_USER || 'vtan69290@gmail.com';
    const timeString = new Date().toLocaleString('vi-VN', { timeZone: 'Asia/Ho_Chi_Minh' });

    const mailOptions = {
        from: `"Hệ thống HTTH" <${process.env.EMAIL_USER || 'vtan69290@gmail.com'}>`,
        to: adminEmail,
        subject: `[Nạp Tiền] Yêu cầu nạp tiền mới từ ${username}`,
        html: `
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #eee; border-radius: 10px;">
                <h2 style="color: #2e7d32; border-bottom: 2px solid #2e7d32; padding-bottom: 10px;">Yêu cầu nạp tiền mới</h2>
                <p>Xin chào Admin,</p>
                <p>Hệ thống vừa nhận được một yêu cầu nạp tiền mới với thông tin chi tiết dưới đây:</p>
                <table style="width: 100%; border-collapse: collapse; margin-top: 15px;">
                    <tr>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;">Tài khoản:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd;">${username}</td>
                    </tr>
                    <tr>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;">Số tiền:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; color: #d32f2f; font-weight: bold;">${amount.toLocaleString('vi-VN')}đ VNĐ</td>
                    </tr>
                    <tr>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;">Nội dung chuyển khoản:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; font-family: monospace; font-size: 1.1em; color: #1565c0; font-weight: bold;">${transferContent}</td>
                    </tr>
                    <tr>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;">Mã đơn nạp:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; font-family: monospace;">${code}</td>
                    </tr>
                    <tr>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd; font-weight: bold;">Thời gian tạo:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #ddd;">${timeString}</td>
                    </tr>
                </table>
                <p style="margin-top: 20px; color: #555; font-size: 0.9em;">
                    <i>Đây là thông báo tự động từ hệ thống GameHTTH. Vui lòng không trả lời email này.</i>
                </p>
            </div>
        `
    };

    try {
        const info = await transporter.sendMail(mailOptions);
        console.log(`[Email] Đã gửi thông báo nạp tiền thành công tới ${adminEmail}: ${info.messageId}`);
        return { success: true, messageId: info.messageId };
    } catch (error) {
        console.error('[Email Error] Lỗi khi gửi mail thông báo nạp tiền:', error);
        return { success: false, error: error.message };
    }
}

module.exports = {
    sendAdminDepositNotification
};
