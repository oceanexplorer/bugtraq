<template>
    <BNavbar class="mb-3" toggleable="lg" variant="info" data-bs-theme="dark">
        <BNavbarBrand to="/">
            <font-awesome-icon icon="bug"/>
            <span class="ps-2">BugTraq</span>
        </BNavbarBrand>
        <BNavbarToggle target="nav-collapse"/>
        <BCollapse id="nav-collapse" is-nav>
            <BNavbarNav>
                <BNavItem to="/">Bugs</BNavItem>
                <BNavItem to="/users">Users</BNavItem>
            </BNavbarNav>
        </BCollapse>
    </BNavbar>
</template>

<script setup>
    import { BCollapse, BNavbar, BNavbarBrand, BNavbarNav, BNavbarToggle, BNavItem } from 'bootstrap-vue-next'
</script>
