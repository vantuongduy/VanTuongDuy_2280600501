const API_URL = "https://localhost:7294/api/products"; // Đổi thành đúng API của bạn

const danhSachSanPhamElement = document.getElementById("danhSachSanPham");

// Hàm tạo giao diện sản phẩm
function createProductCard(product) {
    const productCardElement = document.createElement("div");
    productCardElement.classList.add("col");

    productCardElement.innerHTML = `
        <div class="card">
            <img src="https://picsum.photos/600/400" class="card-img-top" alt="Ảnh sản phẩm">
            <div class="card-body">
                <h5 class="card-title">${product.name}</h5>
                <p class="card-text">${product.description || "Không có mô tả"}</p>
                <p class="card-text">Giá: ${product.price} VND</p>
                <a href="#" class="btn btn-primary">Mua Ngay</a>
            </div>
        </div>
    `;

    return productCardElement;
}

// Hàm lấy danh sách sản phẩm
function fetchProducts() {
    fetch(API_URL)
        .then(response => {
            if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);
            return response.json();
        })
        .then(products => {
            danhSachSanPhamElement.innerHTML = "";
            products.forEach(product => {
                const productCardElement = createProductCard(product);
                danhSachSanPhamElement.appendChild(productCardElement);
            });
        })
        .catch(error => console.error("Lỗi lấy sản phẩm:", error));
}

// Khi trang tải xong thì gọi API
document.addEventListener("DOMContentLoaded", fetchProducts);
function createProductCard(product) {
    const productCardElement = document.createElement("div");
    productCardElement.classList.add("col");

    productCardElement.innerHTML = `
        <div class="card">
            <img src="https://picsum.photos/600/400" class="card-img-top" alt="Ảnh sản phẩm">
            <div class="card-body">
                <h5 class="card-title">${product.name}</h5>
                <p class="card-text">${product.description || "Không có mô tả"}</p>
                <p class="card-text">Giá: ${product.price} VND</p>
                <button class="btn btn-warning" onclick="editProduct(${product.id})">Sửa</button>
                <button class="btn btn-danger" onclick="deleteProduct(${product.id})">Xóa</button>
            </div>
        </div>
    `;

    return productCardElement;
}
async function deleteProduct(id) {
    if (!confirm("Bạn có chắc muốn xóa sản phẩm này không?")) return;

    try {
        const response = await fetch(`https://localhost:7294/api/products/${id}`, { method: "DELETE" });

        if (response.ok) {
            alert("Xóa sản phẩm thành công!");
            fetchProducts(); // Cập nhật danh sách sau khi xóa
        } else {
            alert("Xóa sản phẩm thất bại!");
        }
    } catch (error) {
        console.error("Lỗi khi xóa sản phẩm:", error);
    }
}
function editProduct(id) {
    fetch(`https://localhost:7294/api/products/${id}`)
        .then(response => response.json())
        .then(product => {
            document.getElementById("editId").value = product.id;
            document.getElementById("editName").value = product.name;
            document.getElementById("editPrice").value = product.price;
            document.getElementById("editDescription").value = product.description || "";

            document.getElementById("editModal").style.display = "block";
        })
        .catch(error => console.error("Lỗi lấy sản phẩm:", error));
}

function closeEditModal() {
    document.getElementById("editModal").style.display = "none";
}
async function updateProduct() {
    const id = document.getElementById("editId").value;
    const updatedProduct = {
        id: parseInt(id),
        name: document.getElementById("editName").value,
        price: parseFloat(document.getElementById("editPrice").value),
        description: document.getElementById("editDescription").value
    };

    try {
        const response = await fetch(`https://localhost:7294/api/products/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedProduct)
        });

        if (response.ok) {
            alert("Cập nhật thành công!");
            closeEditModal();
            fetchProducts(); // Cập nhật lại danh sách
        } else {
            alert("Cập nhật thất bại!");
        }
    } catch (error) {
        console.error("Lỗi cập nhật sản phẩm:", error);
    }
}
async function addProduct() {
    const newProduct = {
        name: document.getElementById("newName").value,
        price: parseFloat(document.getElementById("newPrice").value),
        description: document.getElementById("newDescription").value
    };

    try {
        const response = await fetch("https://localhost:7294/api/products", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newProduct)
        });

        if (response.ok) {
            alert("Thêm sản phẩm thành công!");
            fetchProducts();
        } else {
            alert("Thêm sản phẩm thất bại!");
        }
    } catch (error) {
        console.error("Lỗi thêm sản phẩm:", error);
    }
}

